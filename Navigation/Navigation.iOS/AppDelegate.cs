using System;
using System.Collections.Generic;
using CarouselView.FormsPlugin.iOS;
using CoreLocation;
using Foundation;
using Navigation.Enums;
using Navigation.Models;
using UIKit;

namespace Navigation.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        public static CLLocationManager LocationManager;
        public static CLBeaconRegion BeaconsRegion;
        NSUuid beaconUUID;

        public static List<CLBeaconRegion> BeaconsRegionsList;
        public static CLBeaconRegion BigBeaconRegion;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
            NSString stringForBar = new NSString("statusBar");
            UIView statusBar = UIApplication.SharedApplication.ValueForKey(stringForBar) as UIView;

            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = new UIColor(red: 0.10f, green: 0.10f, blue: 0.18f, alpha: 1.0f);
                statusBar.TintColor = UIColor.Red;
            }

            global::Xamarin.Forms.Forms.Init();
            CarouselViewRenderer.Init();

            SetUpLocationManger();
           
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

        public void SetUpLocationManger()
        {
            beaconUUID = new NSUuid(BeaconCreds.UUID);

            LocationManager = new CLLocationManager();
            LocationManager.RequestAlwaysAuthorization();
            LocationManager.StartUpdatingHeading();

            if(BeaconCreds.BeaconMonitoring == BeaconMonitoring.Proximity)
            {
                BeaconsRegion = new CLBeaconRegion(beaconUUID, BeaconCreds.BeaconsRegion);

                LocationManager.StartRangingBeacons(BeaconsRegion);
            }
            else
            {
                BeaconsRegionsList = new List<CLBeaconRegion>();

                foreach(Beacon beacon in Beacons.BeaconList)
                {
                    CLBeaconRegion tempRegion = new CLBeaconRegion(beaconUUID, (ushort)beacon.Major, (ushort)beacon.Minor, beacon.RegionId);
                    tempRegion.NotifyEntryStateOnDisplay = true;
                    tempRegion.NotifyOnEntry = true;
                    tempRegion.NotifyOnExit = true;

                    BeaconsRegionsList.Add(tempRegion);

                    LocationManager.StartMonitoring(tempRegion);
                }
            }
        }
    }
}
