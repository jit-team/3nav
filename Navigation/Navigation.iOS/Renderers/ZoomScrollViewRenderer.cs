using Navigation.CustomUIElements;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ZoomScroolView), typeof(Navigation.iOS.Renderers.ZoomScrollViewRenderer))]
namespace Navigation.iOS.Renderers
{
    public class ZoomScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            UIScrollView native = NativeView as UIScrollView;
            native.MaximumZoomScale = 3f;
            native.MinimumZoomScale = 1f;

            native.ViewForZoomingInScrollView = ScrollSubView;

            native.ShowsHorizontalScrollIndicator = false;
            native.ShowsVerticalScrollIndicator = false;

            UITapGestureRecognizer tap = new UITapGestureRecognizer(() => {
                native.SetZoomScale(0, true);
            });

            tap.NumberOfTapsRequired = 2;

            if (native.Subviews.Length > 0 && native.Subviews[0] != null)
            {
                if (native.Subviews[0].GestureRecognizers == null)
                    native.Subviews[0].AddGestureRecognizer(tap);
            }
        }
        public UIView ScrollSubView(UIScrollView sv) 
        {
            return sv.Subviews[0];
        }
    }
}
