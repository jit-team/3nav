using System;
using Navigation.Interfaces;
using Navigation.Events;
using Xamarin.Forms;
using Navigation.Droid.Services;
using static Navigation.Events.BeaconRangingEvent;
using static Navigation.Events.BeaconProximityEvent;
using Navigation.Models;
using Navigation.Enums;
using System.Collections.Generic;
using RadiusNetworks.IBeaconAndroid;
using System.Linq;

[assembly: Dependency(typeof(BeaconService))]
namespace Navigation.Droid.Services
{
    public class BeaconService : IBeaconService
    {
        public event BeaconRangingHandler OnBeaconRangingDataChanged;
        public event BeaconProximityHandler OnBeaconProximityDataChanged;

        BeaconRangingEvent _beaconRangingEventArgs;
        BeaconProximityEvent _beaconProximityEventArgs;

        public void Subscribe()
        {
            if(BeaconCreds.BeaconMonitoring == BeaconMonitoring.Ranging)
            {
                SetUpForBeaconsRanging();
            }
            else
            {
                SetUpForBeaconsProximity();
            }
        }

        private void SetUpForBeaconsProximity()
        {
            _beaconProximityEventArgs = new BeaconProximityEvent();
            MainActivity.BeaconsRangeNotifier.DidRangeBeaconsInRegionComplete += OnBeaconsFound;
        }

        private void SetUpForBeaconsRanging()
        {
            MainActivity.BeaconsMonitorNotifier.EnterRegionComplete += EnteredRegion;
            MainActivity.BeaconsMonitorNotifier.ExitRegionComplete += ExitedRegion;
            _beaconRangingEventArgs = new BeaconRangingEvent();
        }

        public void EnteredRegion(object sender, MonitorEventArgs e)
        {
            if (e.Region != null)
            {
                PrepareRangingEventProperties(true, e.Region.UniqueId);
            }
        }

        public void ExitedRegion(object sender, MonitorEventArgs e)
        {
            if (e.Region != null)
            {
                PrepareRangingEventProperties(false, e.Region.UniqueId);
            }
        }

        private void OnBeaconsFound(object sender, RangeEventArgs e)
        {
            if (e.Beacons == null || e.Beacons.Count == 0)
                return;

            List<IBeacon> nearestBeacon = GetNearestBeacon(e.Beacons);

            if (nearestBeacon != null)
            {
                PrepareProximityEventProperties(nearestBeacon);
            }
        }

        private List<IBeacon> GetNearestBeacon(ICollection<IBeacon> beacons)
        {
            List<IBeacon> beaconsGraterThenZero = beacons.Where(b => b.Accuracy > 0).ToList();
            beaconsGraterThenZero.Sort((beacon1, beacon2) => beacon1.Accuracy.CompareTo(beacon2.Accuracy));

            return beaconsGraterThenZero;
        }


        void PrepareRangingEventProperties(bool isRegionEntered, string regionId)
        {
            _beaconRangingEventArgs.Enter = isRegionEntered;
            _beaconRangingEventArgs.RegionId = regionId;
            OnBeaconRangingNotify(_beaconRangingEventArgs);
        }

        private void PrepareProximityEventProperties(List<IBeacon> nearestBeacons)
        {
            _beaconProximityEventArgs = new BeaconProximityEvent();
            foreach (IBeacon beacon in nearestBeacons)
            {
                _beaconProximityEventArgs.ListOfBeaconsMinorAndProximity.Add(new ProximityBeacon((int)beacon.Minor, beacon.Accuracy));
            }

            OnBeaconProximityNotify(_beaconProximityEventArgs);
        }

        void OnBeaconProximityNotify(BeaconProximityEvent eventArgs)
        {
            OnBeaconProximityDataChanged(this, eventArgs);
        }

        public void OnBeaconRangingNotify(BeaconRangingEvent eventArgs)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                OnBeaconRangingDataChanged(this, eventArgs);
            });
        }

        public void Unsubscribe()
        {
            if(BeaconCreds.BeaconMonitoring == BeaconMonitoring.Ranging)
            {
                MainActivity.BeaconsMonitorNotifier.EnterRegionComplete -= EnteredRegion;
                MainActivity.BeaconsMonitorNotifier.ExitRegionComplete -= ExitedRegion;
            }
            else
            {
                MainActivity.BeaconsRangeNotifier.DidRangeBeaconsInRegionComplete -= OnBeaconsFound;
            }
        }
    }
}
