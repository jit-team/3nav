using System;
using Navigation.Models;
using Navigation.Services;
using Xamarin.Forms;

namespace Navigation.Pages
{
    public partial class StartPage : MasterDetailPage
    {
        public StartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            IsPresentedChanged += OnPresentedChanged;
            PagesService.Pages[0].Parent = null;
            Detail = PagesService.Pages[0];
        }

        private void OnPresentedChanged(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform.Equals(DeviceType.IOS))
            {
                IsGestureEnabled = IsPresented;
            }
        }
    }
}
