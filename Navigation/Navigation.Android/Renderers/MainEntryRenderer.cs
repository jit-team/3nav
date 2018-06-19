using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Navigation.CustomUIElements;
using Navigation.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MainEntry), typeof(MainEntryRenderer))]
namespace Navigation.Droid.Renderers
{
    class MainEntryRenderer : EntryRenderer
    {
        public MainEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(10, 0, 10, 0);

                Control.SetBackgroundColor(Colors.MainEntryColor);
            }
        }
    }
}
