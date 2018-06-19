using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Navigation.Helpers;
using Navigation.Models;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace Navigation.Services
{
    public class LocationService
    {
        private static Point _schoolLocation = new Point(54.506853, 18.546189);

        private static async Task<Point> GetPosition()
        {
            try
            {
                Position position = null;
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                
                position = await locator.GetLastKnownLocationAsync();
                
                if (position != null)
                    return new Point(position.Latitude, position.Longitude);
                
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                    return new Point(0, 0);
                
                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(3));
                return new Point(position.Latitude, position.Longitude);
            }
            catch(Exception)
            {
                return new Point(0, 0);
            }
        }

        public static async Task<double> GetDistanceFromSchool()
        {
            return DistanceHelper.GetDistanceBetweenTwoPointsInMeters(await GetPosition(), _schoolLocation);
        }
    }
}
