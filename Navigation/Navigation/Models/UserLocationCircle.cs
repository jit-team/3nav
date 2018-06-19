namespace Navigation.Models
{
    public static class UserLocationCircle
    {
        private static double _lineOnMapInMeters = 2;
        private static double _beaconsRangeInMeters = 3.5;
        private static double _lineOnMapProportionalVale = 0.2;

        public static double GetDiameterOfACricle()
        {
            return (_beaconsRangeInMeters * _lineOnMapProportionalVale) / _lineOnMapInMeters;
        }
    }    
}
