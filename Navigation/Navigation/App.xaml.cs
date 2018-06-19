using Navigation.Interfaces;
using Xamarin.Forms;

namespace Navigation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Pages.StartPage();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            MessagingCenter.Send(this, Messages.OnResume, 0);
        }
    }
}
