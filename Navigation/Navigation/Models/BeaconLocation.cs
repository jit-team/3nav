using System.Collections.Generic;
using Xamarin.Forms;

namespace Navigation.Models
{
    public class BeaconLocation
    {
        public static List<Classroom> Classrooms = new List<Classroom>
        {
            new Classroom("Aula", new Rectangle(0.825, -0.02, 0.2, 0.2)),
            new Classroom("Sekretariat", new Rectangle(0.68, 0.105, 0.2, 0.2)),
            new Classroom("Pokój nauczycielski", new Rectangle(0.96, 0.12, 0.2, 0.2)),
            new Classroom("Sala 2", new Rectangle(0.27, 0.12, 0.2, 0.2)),
            new Classroom("Sala 8", new Rectangle(0.36, 0.1898, 0.2, 0.2)),
            new Classroom("Sala 9", new Rectangle(0.36, 0.24, 0.2, 0.2)),
            new Classroom("Sala 10", new Rectangle(0.31, 0.29, 0.2, 0.2)),
            new Classroom("Sala 10A", new Rectangle(0.31, 0.29, 0.2, 0.2)),
            new Classroom("Sala 11", new Rectangle(0.31, 0.29, 0.2, 0.2)),

            new Classroom("Sala 12", new Rectangle(0.36, 0.334, 0.2, 0.2)),
            new Classroom("Sala 13", new Rectangle(0.36, 0.39, 0.2, 0.2)),
            new Classroom("Sala 14", new Rectangle(0.31, 0.435, 0.2, 0.2)),
            new Classroom("Sala 14A", new Rectangle(0.31, 0.435, 0.2, 0.2)),
            new Classroom("Sala 15", new Rectangle(0.31, 0.435, 0.2, 0.2)),
            new Classroom("Sala 16", new Rectangle(0.36, 0.482, 0.2, 0.2)),
            new Classroom("Sala 17", new Rectangle(0.36, 0.535, 0.2, 0.2)),
            new Classroom("Sala 18", new Rectangle(0.31, 0.582, 0.2, 0.2)),
            new Classroom("Sala 19", new Rectangle(0.31, 0.582, 0.2, 0.2)),

            new Classroom("Sala 20", new Rectangle(0.36, 0.63, 0.2, 0.2)),
            new Classroom("Sala 21", new Rectangle(0.36, 0.68, 0.2, 0.2)),
            new Classroom("Sala 22", new Rectangle(0.31, 0.72, 0.2, 0.2)),
            new Classroom("Sala 23", new Rectangle(0.31, 0.72, 0.2, 0.2)),
            new Classroom("Sala F2", new Rectangle(0.31, 0.72, 0.2, 0.2)),
            new Classroom("Sala gimnastyczna", new Rectangle(0.9, 0.8, 0.2, 0.2)),
            new Classroom("Sala 24", new Rectangle(0.36, 0.755, 0.2, 0.2)),
            new Classroom("Sala 25", new Rectangle(0.404, 0.807, 0.2, 0.2)),

            new Classroom("Sala 26", new Rectangle(0.404, 0.85, 0.2, 0.2)),
            new Classroom("Sala 27", new Rectangle(0.404, 0.875, 0.2, 0.2)),
            new Classroom("Stołówka", new Rectangle(0.23, 1.04, 0.2, 0.2)),

        };

        static double greenCircleWidth = 0.25;
        public static Rectangle Beacon1 = new Rectangle(0.95,0.16, greenCircleWidth, 0);
        public static Rectangle Beacon3 = new Rectangle(0.56, 0.17, greenCircleWidth, 0);
        public static Rectangle Beacon4 = new Rectangle(0.5, 0.25, greenCircleWidth, 0);
        public static Rectangle Beacon5 = new Rectangle(0.5, 0.34, greenCircleWidth, 0);
        public static Rectangle Beacon6 = new Rectangle(0.5, 0.42, greenCircleWidth, 0);
        public static Rectangle Beacon7 = new Rectangle(0.5, 0.5, greenCircleWidth, 0);
        public static Rectangle Beacon8 = new Rectangle(0.5, 0.59, greenCircleWidth, 0);
        public static Rectangle Beacon9 = new Rectangle(0.5, 0.67, greenCircleWidth, 0);
        public static Rectangle Beacon10 = new Rectangle(0.55, 0.74, greenCircleWidth, 0);
        public static Rectangle Beacon11 = new Rectangle(0.45, 0.84, greenCircleWidth, 0);
        public static Rectangle Beacon12 = new Rectangle(0.45, 0.92, greenCircleWidth, 0);
        public static Rectangle Beacon13 = new Rectangle(0.66, 1, greenCircleWidth, 0);

    }
}
