using System;
using System.ComponentModel;
using System.Runtime.Remoting.Contexts;
using CoreGraphics;
using Navigation.CustomUIElements;
using Navigation.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RotationImage), typeof(RotationImageRenderer))]
namespace Navigation.iOS.Renderers
{
    public class RotationImageRenderer: ImageRenderer
    {
        double newRotation;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            RotationImage image = (RotationImage)sender;
            newRotation = (float)image.NextRotation;

            if(Control != null)
            {
                Animate(0.5, () => 
                {
                    nfloat angle = (nfloat)((newRotation * Math.PI) / 180);
                    Transform = CGAffineTransform.MakeRotation(angle);
                        
                });
            }
		}

	}
}
