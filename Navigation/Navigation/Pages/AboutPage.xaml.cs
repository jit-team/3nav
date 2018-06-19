using Navigation.ViewModels;
using Xamarin.Forms;

namespace Navigation.Pages
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
            NavigationPage.SetHasNavigationBar(this, value: false);
        }

        protected override async void OnAppearing()
        {
            await view.FadeTo(1, 500);
            base.OnAppearing();
        }

        protected override async void OnDisappearing()
        {
            await view.FadeTo(0, 10);
            base.OnDisappearing();
        }
    }
}
