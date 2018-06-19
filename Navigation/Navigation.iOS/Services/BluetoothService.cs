using System;
using CoreBluetooth;
using Foundation;
using Navigation.Interfaces;
using Navigation.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(BluetoothService))]
namespace Navigation.iOS.Services
{
    public class BluetoothService : IBluetoothService
    {
        private CBCentralManager _bluetoothManager;
        public BluetoothService()
        {
            _bluetoothManager = new CoreBluetooth.CBCentralManager();
        }

        public void EnableBluetooth()
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=Bluetooth"));
        }

        public bool IsBluetoothEnabled()
        {
            if(_bluetoothManager.IsScanning)
            {
                return true;
            }
            if (_bluetoothManager.State == CBCentralManagerState.PoweredOff)
            {
                return false;
            }
            return true; 
        }
    }
}
