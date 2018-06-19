using System;
using System.ComponentModel;
using Android.Content;
using Android.Views.Animations;
using Navigation.CustomUIElements;
using Navigation.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RotationImage), typeof(RotationImageRenderer))]
namespace Navigation.Droid.Renderers
{
    public class RotationImageRenderer : ImageRenderer
    {
        float currentDegree = 0;

        public RotationImageRenderer(Context context) : base(context)
        {
        }

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            base.OnElementPropertyChanged(sender, e);

            RotationImage image = (RotationImage)sender;
            float newRotation = (float)(image.NextRotation % 360);

            if (Control != null)
            {
                RotateAnimation animation = new RotateAnimation((float)currentDegree, (float)newRotation,
                                                                Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf,
                                                               0.5f);
                animation.Duration = 250;
                animation.FillAfter = true;
                Control.StartAnimation(animation);
                currentDegree = (float)newRotation;
            }
		}
	}
}

