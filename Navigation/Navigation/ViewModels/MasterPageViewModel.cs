using System.Collections.Generic;
using System.Threading.Tasks;
using Navigation.Pages;
using Navigation.Services;
using Xamarin.Forms;

namespace Navigation.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {
        public List<MenuItem> Items { get; set; }
        private enum _page { Main, About, Selfguide };

        private MenuItem _selectedItem;
        public MenuItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                NavigateToNextPage(_selectedItem.Id, value.Id);
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }
        public MasterPageViewModel()
        {
            Items = new List<MenuItem>
            {
                new MenuItem("Mapa", 0),
                new MenuItem("O aplikacji", 1),
                new MenuItem("Samouczek", 2)
            };
            _selectedItem = Items[0];
            SelectedItem = Items[0];

            MessagingCenter.Subscribe<Pages.MainPage, bool>(this, Messages.IsFirstLaunchPopupMessage, (sender, arg) =>
            {
                if (arg)
                {
                    var mdp = (MasterDetailPage)Application.Current.MainPage;
                    mdp.Detail = PagesService.Pages[(int)_page.Main];
                    SelectedItem = Items[2];
                }
            });

            MessagingCenter.Subscribe<SelfGuideViewModel, int>(this, Messages.ChangeCurrentListElement, (sender, arg) =>
            {
                SelectedItem = Items[arg];
            });
        }

        private void NavigateToNextPage(int currentPage, int destinationPage)
        {
            if (currentPage != destinationPage)
            {
                var mdp = (MasterDetailPage)Application.Current.MainPage;

                switch (destinationPage)
                {
                    case 0:
                        ChangePage(mdp, (int)_page.Main);
                        break;
                    case 1:
                        ChangePage(mdp, (int)_page.About);
                        break;
                    case 2:
                        ChangePage(mdp, (int)_page.Selfguide);
                        break;
                }
            }
        }

        private void ChangePage(MasterDetailPage mdp, int page)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (page != 2)
                    mdp.Detail = PagesService.Pages[page];
                else
                    mdp.Detail = new SelfGuidedPage();
                
                await Task.Delay(250);
                mdp.IsPresented = false;
            });
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public MenuItem(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}