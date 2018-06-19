using System;
using System.Threading.Tasks;
using Navigation.Enums;
using Navigation.Events;
using Navigation.Interfaces;
using Navigation.Models;
using Navigation.Services;
using Navigation.ViewModels;
using Xamarin.Forms;
using static Navigation.Events.BeaconProximityEvent;
using static Navigation.Events.BeaconRangingEvent;

namespace Navigation.Pages
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _vm;
        private double _mapSize = -1;

        private bool _isNavigationAlertShowed = false;
        public bool IsNavigationAlertShowed
        {
            get => _isNavigationAlertShowed;
            set
            {
                _isNavigationAlertShowed = value;
                BluetoothAlert();
            }
        }

        private bool _isTutorialAlertShowed = false;
        public bool IsTutorialAlertShowed
        {
            get => _isTutorialAlertShowed;
            set
            {
                _isTutorialAlertShowed = value;
                NavigationAlert();
            }
        }

        int _navigationCount = 0;
        int _tutorialCount = 0;
        int _bluetoothCount = 0;
        IBluetoothService _iBluetoothService;

        private void Handle_Focused(object sender, FocusEventArgs e)
        {
            _vm.SetListVisibility(true);
        }

        private void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            _vm.HideList();
        }

        public MainPage()
        {
            InitializeComponent();

            _vm = new MainPageViewModel();
            _iBluetoothService = DependencyService.Get<IBluetoothService>();
            BindingContext = _vm;
            NavigationPage.SetHasNavigationBar(this, value: false);

            MessagingCenter.Subscribe<App, int>(this, Messages.OnResume, (sender, arg) =>
            {
                StartServices();
            });
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var item = e.Item.ToString();
                ClassroomList.SelectedItem = false;
                _vm.ClassroomItemTap((Classroom)e.Item);
            }
        }

        private void NavigationAlert()
        {
            if (IsTutorialAlertShowed)
            {
                if(!_vm.IsUserNearToSchool)
                {
                    if (_navigationCount < 1)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            var answer = await DisplayAlert("Nie jesteś w pobliżu szkoły", "Czy chcesz włączyć nawigację?", "Tak", "Nie");
                            if (answer)
                            {
                                if (Device.RuntimePlatform.Equals(DeviceType.Android))
                                    Device.OpenUri(new Uri("http://maps.google.com/maps?f=d&daddr=54.506853,18.546189"));
                                
                                else if (Device.RuntimePlatform.Equals(DeviceType.IOS))
                                    Device.OpenUri(new Uri("http://maps.apple.com/?daddr=54.506853,18.546189"));
                            }
                            if (!IsNavigationAlertShowed)
                                IsNavigationAlertShowed = true;
                        });
                        _navigationCount++;
                    }
                    else if (!IsNavigationAlertShowed)
                        IsNavigationAlertShowed = true;
                }
            }
        }

        private void TutorialAlert()
        {
            if (SharedPreferencesService.IsFirstLaunch)
            {
                if (_tutorialCount < 1)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Delay(500);
                        var answer = await DisplayAlert("Witaj w aplikacji!", "Czy chcesz uruchomić samouczek?", "Tak", "Nie");
                        MessagingCenter.Send<MainPage, bool>(this, Messages.IsFirstLaunchPopupMessage, answer);
                        if (!IsTutorialAlertShowed)
                            IsTutorialAlertShowed = true;
                    });
                    _tutorialCount++;
                }
            }
            else if (!IsTutorialAlertShowed)
                IsTutorialAlertShowed = true;
        }

        private void BluetoothAlert()
        {
            if(IsNavigationAlertShowed)
            {
                if(!_iBluetoothService.IsBluetoothEnabled())
                {
                    if(_bluetoothCount < 1)
                    {
                        _bluetoothCount++;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Delay(500);
                            var answer = await DisplayAlert("Bluetooth wyłączony.", "Czy chcesz włączyć Bluetooth?", "Tak", "Nie");
                            if (answer)
                            {
                                _iBluetoothService.EnableBluetooth();
                                await Task.Delay(5000);

                                if (_iBluetoothService.IsBluetoothEnabled())
                                    _vm.IsBluetoothEnabled = false;
                            }
                            else
                            {
                                _vm.SetBluetoothStatus(false);
                            }
                        });
                    }
                    else
                        _bluetoothCount = 0;
                }
                Map.HeightRequest = _mapSize;
            }
        }
        
        protected override async void OnAppearing()
        {
            StartServices();

            await view.FadeTo(1, 500);

            await _vm.CheckIfSchoolNearby();
            TutorialAlert();

            if (!_iBluetoothService.IsBluetoothEnabled())
                BluetoothAlert();
            else
                _vm.IsBluetoothEnabled = false;

            base.OnAppearing();
        }

        private void StartServices()
        {
            if(_iBluetoothService.IsBluetoothEnabled())
            {
                _vm.IsBluetoothEnabled = false;
            }

            if(BeaconCreds.BeaconMonitoring == BeaconMonitoring.Ranging)
            {
                _vm.BeaconService.OnBeaconRangingDataChanged += new BeaconRangingHandler(_vm.BeaconRangingNotified);
            }
            else
            {
                _vm.BeaconService.OnBeaconProximityDataChanged += new BeaconProximityHandler(_vm.BeaconProximityNotified);
            }


            _vm.CompassService.OnCompassDataChanged += new CompassHandler(_vm.OnCompassDataNotified);

            _vm.BeaconService.Subscribe();
            _vm.CompassService.Start();
        }

        protected override async void OnDisappearing()
        {
            _vm.BeaconService.Unsubscribe();
            _vm.CompassService.Stop();
            await view.FadeTo(0, 10);
            base.OnDisappearing();
        }

        private void Handle_SizeChanged(object sender, System.EventArgs e)
        {
            Map.WidthRequest = JitMap.Width;

            if(Device.RuntimePlatform.Equals(DeviceType.IOS))
                Map.HeightRequest = JitMap.Height;

            if (_mapSize < 0)
                _mapSize = JitMap.Height;

            SetHeightToWidthRatio();
        }

        private void SetHeightToWidthRatio()
        {
            _vm.HeightToWidthRatio =  JitMap.Height / JitMap.Width;
        }
    }
}
