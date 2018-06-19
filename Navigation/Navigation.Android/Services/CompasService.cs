using System;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using Android.Views;
using Navigation.Droid.Services;
using Navigation.Events;
using Navigation.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(CompassService))]
namespace Navigation.Droid.Services
{
    public class CompassService : Java.Lang.Object, ICompassService, ISensorEventListener
    {
        CompassEvent data = new CompassEvent();

        public event CompassHandler OnCompassDataChanged;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            data.Angle = Math.Round(e.Values[0]);
            OnCompassDataChanged(this, data);
        }

        public void Start()
        {
            MainActivity.MSensorManager.RegisterListener(this,
                                                         MainActivity.MSensorManager.GetDefaultSensor(SensorType.Orientation), 
                                                         SensorDelay.Fastest);
        }

        public void Stop()
        {
            MainActivity.MSensorManager.UnregisterListener(this);
        }

    }
}

