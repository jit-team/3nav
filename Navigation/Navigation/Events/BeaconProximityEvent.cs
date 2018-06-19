using System;
using System.Collections.Generic;
using Navigation.Models;

namespace Navigation.Events
{
    public class BeaconProximityEvent : EventArgs
    {
        public List<ProximityBeacon> ListOfBeaconsMinorAndProximity = new List<ProximityBeacon>();

        public delegate void BeaconProximityHandler(object sender, BeaconProximityEvent e);
    }
}
