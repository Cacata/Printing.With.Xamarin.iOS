using AppPrinterTest.Helper;
using AppPrinterTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrinterTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();
            LsDevice.ItemSelected += OnItemselect;
        }

        protected async void OnItemselect(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Bluetooth Connection", "Do you want to connect this device?", "Yes", "No");
            if (answer)
            {
                GloVa.BluetoothControl.ParingDevice((RemoteDevice)LsDevice.SelectedItem);
            }
        }

        private void FoundDevices()
        {
            LsDevice.ItemsSource = GloVa.DevicesDiscovered.ToList();
        }

        //Evento para limpiar la lista de tickets.
        protected void OnButtonPrint(object sender, EventArgs e)
        {
            PrintTickets();
        }

        //Evento para limpiar la lista de tickets.
        protected async void OnButtonSearch(object sender, EventArgs e)
        {
            if (!GloVa.BluetoothControl.BluetoothState())
            {
                var answer = await DisplayAlert("Bluetooth is Shut Down", "Do you want to turn on bluetooth for this device?", "Yes", "No");
                if (answer)
                {
                    GloVa.BluetoothControl.TurnOnBluetooth();
                }
            }
            GloVa.BluetoothControl.PrepareAdapter(FoundDevices);
            GloVa.BluetoothControl.SearchDevices();
        }

        //Evento para limpiar la lista de tickets.
        protected void OnButtonDisconnect(object sender, EventArgs e)
        {
        }

        private void PrintTickets()
        {
            GloVa.BluetoothControl.Print("Sale");
        }
    }
}
