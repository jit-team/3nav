using System;
using Navigation.Models;

namespace Navigation.Helpers
{
    public class DistanceHelper
    {
        public static double GetDistanceBetweenTwoPointsInMeters(Point first, Point second)
        {
            var R = 6378137;
            var dLat = GetRadianForAngle(second.Latitude - first.Latitude);
            var dLong = GetRadianForAngle(second.Longitude - first.Longitude);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                           + Math.Cos(GetRadianForAngle(first.Latitude)) * Math.Cos(GetRadianForAngle(second.Latitude))
                                                                                 * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d;
        }

        private static double GetRadianForAngle(double angle)
        {
            return (Math.PI * angle) / 180.0;
        }
    }
}
