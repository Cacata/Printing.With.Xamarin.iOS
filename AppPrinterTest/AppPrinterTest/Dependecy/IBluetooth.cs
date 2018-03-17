using AppPrinterTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPrinterTest.Dependecy
{
    public delegate void DeviceFoundEventHandler();

    public interface IBluetooth
    {
        void ParingDevice(RemoteDevice device);
        void TurnOnBluetooth();
        bool BluetoothState();
        void PrepareAdapter(DeviceFoundEventHandler foundDevices);
        void SearchDevices();
        void Print(string v);
    }
}
