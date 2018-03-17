using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppPrinterTest.Dependecy;
using AppPrinterTest.Models;
using Foundation;
using UIKit;

namespace AppPrinterTest.iOS.Dependecy
{
    public class BluetoothHelper : IBluetooth
    {
        public bool BluetoothState()
        {
            throw new NotImplementedException();
        }

        public void ParingDevice(RemoteDevice device)
        {
            throw new NotImplementedException();
        }

        public void PrepareAdapter(DeviceFoundEventHandler foundDevices)
        {
            throw new NotImplementedException();
        }

        public void Print(string v)
        {
            throw new NotImplementedException();
        }

        public void SearchDevices()
        {
            throw new NotImplementedException();
        }

        public void TurnOnBluetooth()
        {
            throw new NotImplementedException();
        }
    }
}