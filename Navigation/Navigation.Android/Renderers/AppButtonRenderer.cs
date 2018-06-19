using System;
using Android.Content;
using Navigation.CustomUIElements;
using Navigation.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AppButton), typeof(AppButtonRenderer))]
namespace Navigation.Droid.Renderers
{
    public class AppButtonRenderer : ButtonRenderer
    {
        public AppButtonRenderer(Context context) : base(context)
        { 
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackgroundResource(Resource.Drawable.button);
            }
        }
    }
}
