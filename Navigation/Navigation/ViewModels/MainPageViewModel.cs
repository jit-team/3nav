using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Navigation.Events;
using Navigation.Interfaces;
using Navigation.Models;
using Navigation.Services;
using Xamarin.Forms;

namespace Navigation.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        private string _classroom;
        public string EntryClassroom
        {
            get => _classroom;
            set
            {
                _classroom = value;
                ChangeFilteredClassroomList();
                RaisePropertyChanged();
            }
        }

        private string _selectedClassroom;
        public string SelectedClassroom
        {
            get => _selectedClassroom;
            set{ _selectedClassroom = value; RaisePropertyChanged(); }
        }

        private List<Classroom> _filteredClassroomList;
        public List<Classroom> FilteredClassroomList
        {
            get => _filteredClassroomList;
            set { _filteredClassroomList = value.ToList(); RaisePropertyChanged(); }
        }

        private bool _isListVisible;
        public bool IsListVisible
        {
            get => _isListVisible;
            set { _isListVisible = value; RaisePropertyChanged(); }
        }

        public ICommand DismissListCommand { get; private set; }
              
        private Rectangle _userCirclePosition;
        public Rectangle UserCirclePosition
        {
            get => _userCirclePosition;
            set { _userCirclePosition = value; RaisePropertyChanged(); }
        }

        bool _isUserCircleVisible = false;
        public bool IsUserCircleVisible
        {
            get => _isUserCircleVisible;
            set { _isUserCircleVisible = value; RaisePropertyChanged(); }
        }

        private bool _isDestinationVisible = false;
        public bool IsDestinationVisible
        {
            get => _isDestinationVisible;
            set { _isDestinationVisible = value; RaisePropertyChanged(); }
        }

        private Rectangle _destinationPosition;
        public Rectangle DestinationPosition
        {
            get => _destinationPosition;
            set { _destinationPosition = value; RaisePropertyChanged(); }
        }

        private bool _isBluetoothEnabled = false;
        public bool IsBluetoothEnabled
        {
            get => _isBluetoothEnabled;
            set { _isBluetoothEnabled = value; RaisePropertyChanged(); }
        }      

        private bool _isUserNearToSchool;
        public bool IsUserNearToSchool
        {
            get => _isUserNearToSchool;
            set { _isUserNearToSchool = value; }
        }  

        private Rectangle _directionPinPosition;
        public Rectangle DirectionPinPosition
        {
            get => _directionPinPosition;
            set { _directionPinPosition = value; RaisePropertyChanged(); }
        }

        private bool _isDirectionPinVisible = false;
        public bool IsDirectionPinVisible
        {
            get => _isDirectionPinVisible;
            set { _isDirectionPinVisible = value; RaisePropertyChanged(); }
        }

        private double _userDirection = 0;
        public double UserDirection
        {
            get => _userDirection;
            set { _userDirection = value; RaisePropertyChanged(); }
        }

        public double Width { get; set; }
        public double Height { get; set; }

        private List<Classroom> _classroomList;


        private IBluetoothService _iBluetoothService;
        private string _currentBeaconRegionId;      
        private IKeyboardService _keyboardHelper;      
        public IBeaconService BeaconService;      
        public ICompassService CompassService;
        private const double _angleBettwenBuildingAndNorth = -278;
        public double HeightToWidthRatio { get; set; }

        public MainPageViewModel()
        {
            SetDependencyServices();
            SetClassrooms();
            _isListVisible = false;
            _isDestinationVisible = false;
            DismissListCommand = new Command(DismissList);

            if (!_iBluetoothService.IsBluetoothEnabled())
            {
                IsBluetoothEnabled = true;
            }
        }

        private void SetDependencyServices()
        {
            _keyboardHelper = DependencyService.Get<IKeyboardService>();
            BeaconService = DependencyService.Get<IBeaconService>();
            //BeaconService = new BeaconSimulation();
            _iBluetoothService = DependencyService.Get<IBluetoothService>();
            CompassService = DependencyService.Get<ICompassService>();
        }

        private void SetClassrooms()
        {
            _classroomList = new List<Classroom>();
            foreach(var cls in BeaconLocation.Classrooms)
            {
                _classroomList.Add(new Classroom(cls.Name, cls.Region));
            }
        }

        public void SetListVisibility(bool visibility)
        {
            IsListVisible = visibility;
            ChangeFilteredClassroomList();
        }

        public void ClassroomItemTap(Classroom classroom)
        {
            IsListVisible = false;
            EntryClassroom = "";
            IsDestinationVisible = true;
            DestinationPosition = classroom.Region;
            SelectedClassroom = classroom.Name;
            
            if (Device.RuntimePlatform.Equals(DeviceType.IOS))
                _keyboardHelper.HideKeyboard();
        }

        public void SetBluetoothStatus(bool isEnabled)
        {
            IsBluetoothEnabled = !isEnabled;
        }

        public void BeaconRangingNotified(object source, BeaconRangingEvent e)
        {
            var beacon = Beacons.BeaconList.Where(b => b.RegionId.Equals(e.RegionId)).FirstOrDefault();

            if (beacon != null)
            {
                if (e.Enter)
                {
                    Rectangle beaconRectangle = beacon.Region;

                    beaconRectangle.Height = beaconRectangle.Width / HeightToWidthRatio;

                    UserCirclePosition = beaconRectangle;
                    _currentBeaconRegionId = beacon.RegionId;

                    DirectionPinPosition = new Rectangle(0.5, 0.5, 0.75, 0.75);

                    SetUserCircleAndRotationPinVisibility(true);
                }
                else if(beacon.RegionId.Equals(_currentBeaconRegionId))
                {
                    SetUserCircleAndRotationPinVisibility(false);
                }  
            }
            else
            {
                SetUserCircleAndRotationPinVisibility(false);
            }
        }

        public void BeaconProximityNotified(object source, BeaconProximityEvent e)
        {
            var beacon = Beacons.BeaconList.Where(b => b.Minor.Equals(e.ListOfBeaconsMinorAndProximity[0].Minor)).FirstOrDefault();

            if(beacon != null)
            {
                if (e.ListOfBeaconsMinorAndProximity[0].Distance < 20)
                {
                        Rectangle beaconRectangle = beacon.Region;

                        beaconRectangle.Height = beaconRectangle.Width / HeightToWidthRatio;

                        UserCirclePosition = beaconRectangle;
                        _currentBeaconRegionId = beacon.RegionId;

                        DirectionPinPosition = new Rectangle(0.5, 0.5, 0.75, 0.75);

                        SetUserCircleAndRotationPinVisibility(true);
                }
                else
                {
                    SetUserCircleAndRotationPinVisibility(false);
                }
            }
        }

        private void ChangeFilteredClassroomList()
        {
            if (_classroom == null)
                FilteredClassroomList = _classroomList;
            else
                FilteredClassroomList =
                    _classroomList.Where(e => e.Name.ToLower().Contains(_classroom.ToLower())).ToList();
        }

        public async Task CheckIfSchoolNearby()
        {
            var location = await LocationService.GetDistanceFromSchool();

            if(location > 500)
                IsUserNearToSchool = false;
            else
            {
                IsUserNearToSchool = true;
            }
        }
        
        public void HideList()
        {
            IsListVisible = false;
        }

        public void OnCompassDataNotified(object source, CompassEvent e)
        {
            UserDirection = e.Angle + _angleBettwenBuildingAndNorth;
        }

        private void DismissList()
        {
            if (!IsListVisible)
                EntryClassroom = "";

            IsListVisible = false;
            IsDestinationVisible = false;
            SelectedClassroom = "";
        }
        private void SetUserCircleAndRotationPinVisibility(bool visibility)
        {
            IsUserCircleVisible = visibility;
            IsDirectionPinVisible = visibility;
        }
    }
}
