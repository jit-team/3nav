using Xamarin.Forms;

namespace Navigation.Pages
{
    public partial class Blank : ContentPage
    {
        public Blank()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
