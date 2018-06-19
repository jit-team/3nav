using static Navigation.Events.BeaconProximityEvent;
using static Navigation.Events.BeaconRangingEvent;

namespace Navigation.Interfaces
{
    public interface IBeaconService
    {
        event BeaconRangingHandler OnBeaconRangingDataChanged;
        event BeaconProximityHandler OnBeaconProximityDataChanged;
        void Subscribe();
        void Unsubscribe();
    }
}
