namespace Navigation.Models
{
    public class Point
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public float X { get; set; }
        public float Y { get; set; }

        public Point() { }

        public Point(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
