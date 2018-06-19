using System;
using System.Timers;
using Navigation.Events;
using Navigation.Interfaces;
using Navigation.Models;


namespace Navigation.Services
{
    public class BeaconSimulation : IBeaconService
    {
        public event BeaconRangingEvent.BeaconRangingHandler OnBeaconRangingDataChanged;
        public event BeaconProximityEvent.BeaconProximityHandler OnBeaconProximityDataChanged;

        private Timer _runEventTimer; 
        BeaconRangingEvent _beaconRangingEventArgs;
        BeaconProximityEvent _beaconProximityEventArgs;
        private int _beaconsCounter = 0;

        public void Subscribe()
        {
            _beaconRangingEventArgs = new BeaconRangingEvent();
            _runEventTimer = new Timer(5000);
            _runEventTimer.Elapsed += TimerTick;
            _runEventTimer.Start();

            _beaconRangingEventArgs.Enter = false;
        }

        void OnNotify(bool isEntered, string regionId)
        {
            _beaconRangingEventArgs.Enter = isEntered;
            _beaconRangingEventArgs.RegionId = regionId;
            OnBeaconRangingDataChanged(this, _beaconRangingEventArgs);
        }

        void TimerTick(Object source, ElapsedEventArgs e)
        {
            if((Beacons.BeaconList.Count) == _beaconsCounter)
            {
                _beaconsCounter = 0;
            }
            Beacon beacon = Beacons.BeaconList[_beaconsCounter];
            OnNotify(true, beacon.RegionId);
            _beaconsCounter++;
        }

        public void Unsubscribe()
        {
            _runEventTimer.Elapsed -= TimerTick;
        }
    }
}
