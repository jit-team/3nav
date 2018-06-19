using System;
using CoreLocation;
using CoreMotion;
using Foundation;
using Navigation.Events;
using Navigation.Interfaces;
using Navigation.iOS.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CompassService))]
namespace Navigation.iOS.Services
{
    public class CompassService : ICompassService
    {
        CompassEvent data = new CompassEvent();
        public event CompassHandler OnCompassDataChanged;
        CMAttitude _attitude;

        void OnGyroscopeDataUpdate(CMDeviceMotion gyroData, NSError error)
        {
            if (error == null)
            {
                _attitude = gyroData.Attitude;
                data.Angle = _attitude.Yaw;

                OnCompassDataChanged(this, data);
            }
        }

        public virtual void UpdatedHeading(object sender, CLHeadingUpdatedEventArgs newHeading)
        {
            double value = newHeading.NewHeading.TrueHeading;
            data.Angle = value;
            OnCompassDataChanged(this, data);
        }

        public void Start()
        {
            AppDelegate.LocationManager.UpdatedHeading += UpdatedHeading;
        }

        public void Stop()
        {
            AppDelegate.LocationManager.UpdatedHeading -= UpdatedHeading;	
        }
    }
}
