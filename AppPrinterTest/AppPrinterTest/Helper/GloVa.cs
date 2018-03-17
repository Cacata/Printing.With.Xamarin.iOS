using AppPrinterTest.Dependecy;
using AppPrinterTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPrinterTest.Helper
{
    public static class GloVa
    {
        public static List<RemoteDevice> DevicesDiscovered { get; set; }
        public static IBluetooth BluetoothControl { get; set; }
    }
}
