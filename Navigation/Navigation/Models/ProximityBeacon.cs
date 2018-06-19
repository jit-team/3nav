using System;
namespace Navigation.Models
{
    public class ProximityBeacon
    {
        public int Minor { get; set; }
        public double Distance { get; set; }

        public ProximityBeacon(int minor, double distance)
        {
            Minor = minor;
            Distance = distance;
        }
    }
}
