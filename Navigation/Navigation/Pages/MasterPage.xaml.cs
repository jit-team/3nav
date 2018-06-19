using Navigation.ViewModels;
using Xamarin.Forms;

namespace Navigation.Pages
{
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();
            BindingContext = new MasterPageViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
