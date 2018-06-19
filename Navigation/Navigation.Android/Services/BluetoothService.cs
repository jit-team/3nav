using Android.Bluetooth;
using Navigation.Droid.Services;
using Navigation.Interfaces;
using Android.App;
using Android.Content;

[assembly: Xamarin.Forms.Dependency(typeof(BluetoothService))]
namespace Navigation.Droid.Services
{
    public class BluetoothService : IBluetoothService
    {
        private BluetoothManager _bluetoothManager;
        public BluetoothService()
        {
            _bluetoothManager = (BluetoothManager)Application.Context.GetSystemService(Context.BluetoothService);
        }

        public void EnableBluetooth()
        {
            _bluetoothManager.Adapter.Enable();
        }

        public bool IsBluetoothEnabled()
        {
            return true; //on emulator
            //return _bluetoothManager.Adapter.IsEnabled; // on physical device
        }
    }
}
