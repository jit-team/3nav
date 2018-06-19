using System;

namespace Navigation.Events
{
    public class BeaconRangingEvent : EventArgs
    {
        public string RegionId { get; set; }
        public bool Enter { get; set; }

        public delegate void BeaconRangingHandler(object sender, BeaconRangingEvent e);
    }

}
