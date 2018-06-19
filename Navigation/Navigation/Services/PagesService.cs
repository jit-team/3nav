using System.Collections.Generic;
using Xamarin.Forms;
using Navigation.Pages;

namespace Navigation.Services
{
    public class PagesService
    {
        private static List<ContentPage> _pages;

        public static List<ContentPage> Pages
        {
            get
            {
                CreatePages();
                return _pages;
            }
        }

        private static void CreatePages()
        {
            if(_pages == null)
            {
                _pages = new List<ContentPage>
                {
                    new MainPage(), new AboutPage(), new SelfGuidedPage()
                };
                
            }
        }
    }
}
