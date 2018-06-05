using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPrinterTest.Dependecy;
using AppPrinterTest.Helper;
using AppPrinterTest.Models;
using CoreGraphics;
using Foundation;
using RongtaPrinterSDK;
using RongtaPrinterSDK.Infrastructure;
using UIKit;
using ZXing;
using ZXing.Mobile;

namespace AppPrinterTest.iOS.Dependecy
{
    public class BluetoothHelper : IBluetooth
    {
        private IPrinter _Mydevice;
        private IBluetoothManager _Manager;
        private RemoteDevice _selecteddevice;
        //private ExceptionHandler _Myexception;
        private event DeviceFoundEventHandler _EventHandler;
        //private PrinterHelper PrinterCentral;
        private const string CLR = "\r\n";

        public BluetoothHelper()
        {
            _Manager = BluetoothManager.Instance;
            //_Myexception = new ExceptionHandler();
            //PrinterCentral = new PrinterHelper();
            GloVa.DevicesDiscovered = new List<RemoteDevice>();
        }

        public bool BluetoothState()
        {
            if (_Manager == null || _Mydevice == null)
                return false;
            else
                return true;
        }

        public void EndDiscovery()
        {
            _Manager.StopDiscovery();
        }

        public void ParingDevice(RemoteDevice _selecteddevice)
        {
            try
            {
                _Manager.WhenConnected(OnConnect).Connect(_selecteddevice.MAC);
                this._selecteddevice = _selecteddevice;
                //_Manager.WhenConnected(OnConnect).Connect("63F54A98-F47C-4690-BA31-9F416EE14192");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        void OnConnect(IPrinter printer)
        {
            _Mydevice = printer;            
        }
        void Discconect()
        {
            _Manager.Disconnect();
        }

        public void PrepareAdapter(DeviceFoundEventHandler _devicefuntion)
        {
            _EventHandler += _devicefuntion;
        }

        public void Print(string printType)
        {
            try
            {
                //var a = new UIKit.UIImage("icon.jpg");
                //PrintImage()
                _Mydevice.PrintImage(new UIKit.UIImage("icon.png"), 80, 80, CharacterTextAling.Center);

                _Mydevice.PrintText("IBEROSERVICE", CharacterTextType.PrinterCharacterTextWidth4, CharacterTextAling.Center, true)
                        .PrintText("9564203487", CharacterTextType.PrinterCharacterTextWidth3, CharacterTextAling.Center, true)
                        .NextLine(true)
                        .PrintText("________________________________________________", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Right, true)
                        .NextLine(true)
                        .PrintText(DateTime.Now.Date.ToShortDateString(), CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Right, true)
                        .PrintText("Excurtion: ", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .PrintText("Nado con Delfines.", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Left, false)
                        .PrintText("Tour Language: ", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .PrintText("English.", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Left, false)
                        .PrintText("Adult:", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Right, true)
                        .PrintText("2 ", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Right, false)
                        .PrintText("Child:", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Right, false)
                        .PrintText("2 ", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Right, false)
                        .PrintText("Inf:", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Right, false)
                        .PrintText("0", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Right, false)

                        .PrintText("Tourist Name: ", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .PrintText("Armando.", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Left, false)

                        .PrintText("Language: ", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .PrintText("English.", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Left, false)

                        .PrintText("Meeting Point: ", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Right, true)
                        .PrintText("Rio Merengue.", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Right, false)

                        .PrintText("Room: ", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .PrintText("AB-654.", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Left, false)

                        .PrintText("Pickup Date: ", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Right, true)
                        .PrintText(DateTime.Today.AddDays(2).ToShortTimeString(), CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Right, false)

                        .PrintText("Variation: + $50.00", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .PrintText("SubTotal: $400", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .PrintText("Total: USD $450.00", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .NextLine(true)                        
                        .PrintText("Thanks For Prefered Us.", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Center, true)
                        .NextLine(true)
                        .NextLine(true)
                        .PrintText("IN CASE OF CANCELLATION", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                        .NextLine(true)
                        .PrintText("Within 24 hr (48 hr-flights): 100% refund. Within 12 hr (24 hr-flights): 50% refund. No Show. No refund.", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Left, false)
                        .NextLine(true)
                        .PrintText("________________________________________________", CharacterTextType.PrinterCharacterTextWidth1, CharacterTextAling.Right, true)
                        .NextLine(true)
                        .Execute();
                _Mydevice.PrintImage(getQRCode("9564203487"), 0, 0, CharacterTextAling.Center);
                //// Esta imagen se imprime a parte porque en el caso del texto hay que llamar el execute para que se imprima, en caso de tener texto / imagen / texto
                //// Llamar el execute del texto primero, luego imprimir la imagen y luego agregar el nuevo texto, en el execute se limpia el texto anterior.
                //TurnOffBluetooth();
                //TurnOnBluetooth();
                //Task.Delay(1000);
                /*
                    .PrintText("Within 12 hr (24 hr-flights): 50% refund.", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                    .PrintText("No Show. No refund.", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                    .PrintText("RESPONSABILITY", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, true)
                    .PrintText("All arrangements for transportation, accommodation, transfers and sightseeing, in connection with tours, have been made by IBERDOM, as agent for the passenger upon the express condition that we shall not be held liable for any injury, damage, loss, accident or irregularity which may be caused by any company or person employed in the operation of the tour.", CharacterTextType.PrinterCharacterTextBold, CharacterTextAling.Left, false)
                        
                 */

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private UIImage getQRCode(string tnumber)
        {
            var imageBarcode = new UIImageView(new CGRect(20, 100, 100, 100));

            var barcodeWriter = new BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 5
                }
            };

            var barcode = barcodeWriter.Write(tnumber);

            imageBarcode.Image = barcode;
            return barcode;
        }

        public void SearchDevices()
        {
            try
            {
                _Manager.StartDiscovery((device) =>
                {
                    GloVa.DevicesDiscovered.Add(new RemoteDevice() { Name = device.Name, MAC = device.UUID });
                });
                _EventHandler.Invoke();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void HandleDeviceFinishPrintAsync()
        {
            Console.Write("Everything was good in your impresion process.");
            //_Manager.Disconnect();
        }

        private void HandleFailed(Exception e)
        {
            Console.Write("Esta mierda falló!");
            //_Myexception.getCustomMessage(e.Message, "HandleFailed");
        }

        public void TurnOffBluetooth()
        {
            _Manager.Disconnect();
            _Manager = null;
            _Mydevice = null;
        }

        public void TurnOnBluetooth()
        {
            _Manager = BluetoothManager.Instance;
            ParingDevice(this._selecteddevice);
            //new CBCentralManager(this, DispatchQueue.CurrentQueue);
        }
    }
}