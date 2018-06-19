using System;
using RadiusNetworks.IBeaconAndroid;

namespace Navigation.Droid
{
    public class MonitorEventArgs : EventArgs
    {
        public Region Region { get; set; }

        public int State { get; set; }
    }

    public class MonitorNotifier : Java.Lang.Object, IMonitorNotifier
    {
        public event EventHandler<MonitorEventArgs> DetermineStateForRegionComplete;
        public event EventHandler<MonitorEventArgs> EnterRegionComplete;
        public event EventHandler<MonitorEventArgs> ExitRegionComplete;
        MonitorEventArgs monitorEvent;

        public void DidDetermineStateForRegion(int p0, Region p1)
        {
            OnDetermineStateForRegionComplete(p0, p1);
        }

        public void DidEnterRegion(Region regionEntered)
        {
            OnEnterRegionComplete(regionEntered);
        }

        public void DidExitRegion(Region regionEntered)
        {
            OnExitRegionComplete(regionEntered);
        }

        void OnDetermineStateForRegionComplete(int p0, Region p1)
        {
            if (DetermineStateForRegionComplete != null)
            {
                monitorEvent = new MonitorEventArgs();
                monitorEvent.Region = p1;
                monitorEvent.State = p0;
;               DetermineStateForRegionComplete(this,monitorEvent);
            }
        }

        void OnEnterRegionComplete(Region regionEntered)
        {
            if (EnterRegionComplete != null)
            {
                monitorEvent = new MonitorEventArgs();
                monitorEvent.Region = regionEntered;
                EnterRegionComplete(this, monitorEvent);
            }
        }

        void OnExitRegionComplete(Region regionEntered)
        {
            if (ExitRegionComplete != null)
            {
                monitorEvent = new MonitorEventArgs();
                monitorEvent.Region = regionEntered;
                ExitRegionComplete(this, monitorEvent);
            }
        }
    }
}
