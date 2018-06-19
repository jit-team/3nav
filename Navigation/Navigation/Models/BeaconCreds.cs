using Navigation.Enums;

namespace Navigation.Models
{
    public class BeaconCreds
    {
        public static readonly string UUID = BeaconPrivate.BeaconData.UUID; //put your beacons uuid here
        public static readonly string BeaconsRegion = BeaconPrivate.BeaconData.BeaconsRegion; //put your beacons region here
        public static readonly BeaconMonitoring BeaconMonitoring = BeaconMonitoring.Proximity;
    }
}
