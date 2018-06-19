using Xamarin.Forms;

namespace Navigation.CustomUIElements
{
    public class RotationImage : Image
    {
        public static readonly BindableProperty NextRotationProperty =
            BindableProperty.Create("NextRotation", typeof(double), typeof(RotationImage), 0.0, BindingMode.TwoWay);

        public double NextRotation
        {
            get => (double)GetValue(NextRotationProperty);
            set => SetValue(NextRotationProperty, value);
        }
    }
}
