using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using RadiusNetworks.IBeaconAndroid;
using Navigation.Droid;
using Xamarin.Forms;
using Navigation.Models;
using Navigation.Droid.Services;
using Navigation.Events;
using System.Collections.Generic;
using CarouselView.FormsPlugin.Android;
using Android.Hardware;
using Android.Content;
using Navigation.Enums;

[assembly: Dependency(typeof(MainActivity))]
namespace Navigation.Droid
{
    [Activity(Label = "3Nav", Icon = "@drawable/app_icon_pin_light", Theme = "@style/MyTheme.Splash", MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
              ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IBeaconConsumer
    {
        public static IBeaconManager beaconMgr;
        public static MonitorNotifier BeaconsMonitorNotifier;
        public static Region MonitoringRegion;
        public static List<Region> BeaconsRegionsList;
        public static RangeNotifier BeaconsRangeNotifier;

        private string beaconUUID = BeaconCreds.UUID;
        private Region _regionForProximity;

        public static SensorManager MSensorManager;
        public static Sensor MAccelerometr;
        public static Sensor MMagnetometr;

        public MainActivity()
        {
            SetBeaconsManager();
        }

        public void SetSensorManager()
        {
            MSensorManager = (SensorManager)this.GetSystemService(SensorService);
            MAccelerometr = MSensorManager.GetDefaultSensor(SensorType.RotationVector);
            MMagnetometr = MSensorManager.GetDefaultSensor(SensorType.MagneticField);
            BeaconsMonitorNotifier = new MonitorNotifier();
        }

        public void SetBeaconsManager()
        {
            BeaconsRegionsList = new List<Region>();
            beaconMgr = IBeaconManager.GetInstanceForApplication(this);

            if(BeaconCreds.BeaconMonitoring == BeaconMonitoring.Ranging)
            {
                foreach (Beacon beacon in Beacons.BeaconList)
                {
                    Region tempRegion = new Region(beacon.RegionId, beaconUUID, (Java.Lang.Integer)beacon.Major, (Java.Lang.Integer)beacon.Minor);
                    BeaconsRegionsList.Add(tempRegion);
                }
            }
            else
            {
                _regionForProximity = new Region(BeaconCreds.BeaconsRegion, beaconUUID, null, null);
                BeaconsRangeNotifier = new RangeNotifier();
            }
        }
       
        public void OnIBeaconServiceConnect()
        {
            beaconMgr.SetMonitorNotifier(BeaconsMonitorNotifier);

            if (BeaconCreds.BeaconMonitoring == BeaconMonitoring.Ranging)
            {
                foreach (Region beaconRegion in BeaconsRegionsList)
                {
                    beaconMgr.StartMonitoringBeaconsInRegion(beaconRegion);
                }
            }
            else
            {
                beaconMgr.SetRangeNotifier(BeaconsRangeNotifier);
                beaconMgr.StartMonitoringBeaconsInRegion(_regionForProximity);
                beaconMgr.StartRangingBeaconsInRegion(_regionForProximity);
            }

        }

        protected override void OnDestroy()
        {
            beaconMgr.UnBind(this);

            base.OnDestroy();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);
            CarouselViewRenderer.Init();
            beaconMgr.Bind(this);
            SetSensorManager();
            Android.Graphics.Color barColor = Android.Graphics.Color.ParseColor("#191A2F");
            Window.SetStatusBarColor(barColor);
            LoadApplication(new App());
        }
    }
}
