using Navigation.ViewModels;
using Xamarin.Forms;

namespace Navigation.Pages
{
    public partial class SelfGuidedPage : ContentPage
    {
        public SelfGuidedPage()
        {
            InitializeComponent();
            BindingContext = new SelfGuideViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
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
