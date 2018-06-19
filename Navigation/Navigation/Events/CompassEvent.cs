using System;
namespace Navigation.Events
{
    public class CompassEvent : EventArgs
    {
        public double Angle;
    }

    public delegate void CompassHandler(object sender, CompassEvent e);
}
