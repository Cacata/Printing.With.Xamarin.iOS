using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppPrinterTest.Dependecy;
using AppPrinterTest.Models;
using Java.Lang;

namespace AppPrinterTest.Droid.Dependecy
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










        /// <summary>
        /// This BroadcastReceiver will listen to actionfound
        /// for others bluetooth classic devices.
        /// </summary>
        private class CustomBroadcastReceiver : BroadcastReceiver
        {
            public event DeviceFoundEventHandler DeviceDiscovered;
            private BluetoothHelper _service;
            /// <summary>
            /// The CustomBroadcastReceiver class constructor.
            /// </summary>
            public CustomBroadcastReceiver(DeviceFoundEventHandler _whatdofordevice, BluetoothHelper service)
            {
                this.DeviceDiscovered += _whatdofordevice;
                this._service = service;
                this._service._listdevices = new List<BluetoothDevice>();
                GloVa.DevicesDiscovered = new List<RemoteDevice>();
            }

            /// <summary>
            /// Set the current state of the chat connection.
            /// </summary>
            /// <param name='context'>
            /// The current environment where the action was do it.
            /// </param>
            /// <param name='intent'>
            /// The type of action happened.
            /// </param>
            public override void OnReceive(Context context, Intent intent)
            {
                string action = intent.Action;

                if (BluetoothDevice.ActionFound.Equals(action))
                {
                    BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                    device = _service._myadapter.GetRemoteDevice(device.Address);


                    var a = new RemoteDevice();
                    a.BluetoothType = device.Type.ToString();
                    a.Name = device.Name;
                    a.DeviceAddress = device.Address;
                    a.State = device.BondState.ToString();
                    a.Class = device.Class;

                    //Esto no está validando nada. Hay que arreglarlo.
                    if (_service._listdevices.Select(p => p.Address == a.DeviceAddress).Count() == 0)
                    {
                        GloVa.DevicesDiscovered.Add(a);
                        _service._listdevices.Add(device);
                    }

                    DeviceDiscovered.Invoke();
                }

            }
        }

        /// <summary>
        /// This thread runs while attempting to make an outgoing connection
        /// with a device. It runs straight through; the connection either
        /// succeeds or fails.
        /// </summary>
        private class ConnectWay : Thread
        {
            private BluetoothSocket mmSocket;
            private BluetoothHelper _service;
            private byte[] TextToWrite;
            private ASCIIEncoding encoder;

            public ConnectWay(BluetoothHelper service)
            {
                _service = service;

                try
                {
                    Class self = _service.ConnectedDevice.Class;
                    Class[] paramTypes = new Class[] { Integer.Type };
                    Method m = self.GetMethod("createRfcommSocket", paramTypes);
                    Java.Lang.Object[] pars = new Java.Lang.Object[] { Integer.ValueOf(1) };
                    mmSocket = (m.Invoke(_service.ConnectedDevice, pars) as BluetoothSocket);
                    _service.EndDiscovery();
                    mmSocket.Connect();
                    //mmSocket = device.CreateRfcommSocketToServiceRecord(device.GetUuids().ElementAt(0).Uuid);
                    //mmSocket = JsonConvert.DeserializeObject<BluetoothSocket>(mmDevice.Socket);
                    _service.IsConnected = mmSocket.IsConnected;
                }
                catch (Java.IO.IOException e)
                {
                    _service.IsConnected = mmSocket.IsConnected;
                    throw new Java.IO.IOException("BluetoothHelper.ConnectWay.Constructor", e);
                }

            }

            /// <summary>
            /// To connect the current socket.
            /// </summary>
            public override void Run()
            {
                try
                {
                    _service.ChannelToWrite = new CommunicationWay(mmSocket, _service);
                }
                catch (Java.IO.IOException e)
                {
                    //_service.ConnectionFailed();
                    try
                    {
                        mmSocket.Close();
                    }
                    catch (Java.IO.IOException e2)
                    {
                        throw new Java.IO.IOException("BluetoothHelper.ConnectWay.Run", e2);
                    }

                    throw new Java.IO.IOException("BluetoothHelper.ConnectWay.Run", e);
                }
            }

            /// <summary>
            /// Close the current socket.
            /// </summary>
            public void Cancel()
            {
                try
                {
                    mmSocket.Close();
                }
                catch (Java.IO.IOException e)
                {
                    throw new Java.IO.IOException("BluetoothHelper.ConnectWay.Constructor", e);
                }
            }
        }
    }
}