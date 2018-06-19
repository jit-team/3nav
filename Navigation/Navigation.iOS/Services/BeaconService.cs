using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using CoreLocation;
using Navigation.Events;
using Navigation.Interfaces;
using Navigation.iOS.Services;
using Navigation.Models;
using Xamarin.Forms;
using static Navigation.Events.BeaconProximityEvent;
using static Navigation.Events.BeaconRangingEvent;

[assembly: Dependency(typeof(BeaconService))]
namespace Navigation.iOS.Services
{
    public class BeaconService : IBeaconService
    {
        public event BeaconRangingHandler OnBeaconRangingDataChanged;
        public event BeaconProximityHandler OnBeaconProximityDataChanged;

        BeaconRangingEvent _beaconRangingEventArgs;
        BeaconProximityEvent _beaconProximityEventArgs;

        bool _firstEntry;

        public void Subscribe()
        {
            if(BeaconCreds.BeaconMonitoring == Enums.BeaconMonitoring.Ranging)
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
            AppDelegate.LocationManager.DidRangeBeacons += OnBeaconsFound;
        }

        private void SetUpForBeaconsRanging()
        {
            _firstEntry = true;
            _beaconRangingEventArgs = new BeaconRangingEvent();

            AppDelegate.LocationManager.DidDetermineState += (s, e) =>
            {
                if (_firstEntry)
                {
                    if (e.State == CLRegionState.Inside)
                    {
                        PrepareRangingEventProperties(true, e.Region.Identifier);
                        _firstEntry = false;
                    }
                }
            };

            AppDelegate.LocationManager.RegionLeft += RegionLeftDelegate;
            AppDelegate.LocationManager.RegionEntered += RegionEnteredDelegate;
        }

        public void RegionLeftDelegate(object sender, CLRegionEventArgs e)
        {
            PrepareRangingEventProperties(false, e.Region.Identifier);
        }

        public void RegionEnteredDelegate(object sender, CLRegionEventArgs e)
        {
            PrepareRangingEventProperties(true, e.Region.Identifier);
        }

        private void OnBeaconsFound(object sender, CLRegionBeaconsRangedEventArgs e)
        {
            if (e.Beacons == null || e.Beacons.Length == 0)
                return;
            
            List<CLBeacon> nearestBeacon = GetNearestBeacon(e.Beacons);

            if (nearestBeacon != null && nearestBeacon.Count > 0)
            {
                PrepareProximityEventProperties(nearestBeacon);
            } 
        }

        void PrepareRangingEventProperties(bool isRegionEntered, string regionId)
        {
            _beaconRangingEventArgs.Enter = isRegionEntered;
            _beaconRangingEventArgs.RegionId = regionId;

            OnBeaconRangingNotify(_beaconRangingEventArgs);
        }

        void PrepareProximityEventProperties(List<CLBeacon> nearestBeacons)
        {
            _beaconProximityEventArgs = new BeaconProximityEvent();
            foreach(CLBeacon beacon in nearestBeacons)
            {
                _beaconProximityEventArgs.ListOfBeaconsMinorAndProximity.Add(new ProximityBeacon((int)beacon.Minor, beacon.Accuracy));
            }

            OnBeaconProximityNotify(_beaconProximityEventArgs);
        }

        void OnBeaconRangingNotify(BeaconRangingEvent eventArgs)
        {
            OnBeaconRangingDataChanged(this, eventArgs);
        }

        void OnBeaconProximityNotify(BeaconProximityEvent eventArgs)
        {
            OnBeaconProximityDataChanged(this, eventArgs);
        }

        private List<CLBeacon> GetNearestBeacon(CLBeacon[] listOfBeaconsInRange)
        {
            List<CLBeacon> beaconsGraterThenZero = listOfBeaconsInRange.Where(b => b.Accuracy > 0).ToList();

            return beaconsGraterThenZero;
        }

        public void Unsubscribe()
        {
            if (BeaconCreds.BeaconMonitoring == Enums.BeaconMonitoring.Ranging)
            {
                AppDelegate.LocationManager.RegionLeft -= RegionLeftDelegate;
                AppDelegate.LocationManager.RegionEntered -= RegionEnteredDelegate;
            }
            else
            {
                AppDelegate.LocationManager.DidRangeBeacons -= OnBeaconsFound;
            }

        }
    }
}
