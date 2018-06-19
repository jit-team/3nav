using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using Navigation.CustomUIElements;
using Navigation.iOS.Renderers;

[assembly: ExportRenderer(typeof(MainEntry), typeof(MainEntryRenderer))]
namespace Navigation.iOS.Renderers
{
    public class MainEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = Colors.MainEntryColor;
                Control.BorderStyle = UITextBorderStyle.RoundedRect;
            }
        }
    }
}