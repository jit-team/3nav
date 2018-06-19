using System;
using Navigation.Interfaces;
using Navigation.iOS.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardService))]
namespace Navigation.iOS.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}
