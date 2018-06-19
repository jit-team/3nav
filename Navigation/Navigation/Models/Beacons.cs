using System.Collections.Generic;
using Xamarin.Forms;

namespace Navigation.Models
{
    public static class Beacons
    {
        public static readonly List<Beacon> BeaconList = new List<Beacon>
        {
            new Beacon("1", 1805, 1, BeaconLocation.Beacon1, "1"),
            new Beacon("3", 1805, 3, BeaconLocation.Beacon3, "3"),
            new Beacon("4", 1805, 4, BeaconLocation.Beacon4, "4"),
            new Beacon("5", 1805, 5, BeaconLocation.Beacon5, "5"),
            new Beacon("6", 1805, 6, BeaconLocation.Beacon6, "6"),
            new Beacon("7", 1805, 7, BeaconLocation.Beacon7, "7"),
            new Beacon("8", 1805, 8, BeaconLocation.Beacon8, "8"),
            new Beacon("9", 1805, 9, BeaconLocation.Beacon9, "9"),
            new Beacon("10", 1805, 10, BeaconLocation.Beacon10, "10"),
            new Beacon("11", 1805, 11, BeaconLocation.Beacon11, "11"),
            new Beacon("12", 1805, 12, BeaconLocation.Beacon12, "12"),
            new Beacon("13", 1805, 13, BeaconLocation.Beacon12, "13")
        };
    }

    public class Beacon
    {
        public string Name { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public Rectangle Region { get; set; }
        public string RegionId { get; set; }

        public Beacon(string name, int major, int minor, Rectangle region, string regionId)
        {
            Name = name;
            Major = major;
            Minor = minor;
            Region = region;
            RegionId = regionId;
        }
    }
}
