using Xamarin.Forms;

namespace Navigation.Services
{
    public static class SharedPreferencesService
    {
        public static bool IsFirstLaunch
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("FirstUse"))
                    return false;
                else
                    Application.Current.Properties["FirstUse"] = false;
                    return true;
            }
        }
    }
}
