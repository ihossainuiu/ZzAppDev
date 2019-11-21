using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Geolocator;

namespace ZzAppDev
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LocationPage : ContentPage
    {
        public LocationPage()
        {
            InitializeComponent();
            btnGetLocation.Clicked += BtnGetLocation_Clicked;
        }

        private async void BtnGetLocation_Clicked(object sender, EventArgs e)
        {
            await RetrieveLocation();

        }

        private async Task RetrieveLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            IsBusy = true;
            var position = await locator.GetPositionAsync();
            IsBusy = false;

            txtLat.Text = position.Latitude.ToString();
            txtLong.Text = position.Longitude.ToString();


        }

        private bool _isBusy;
        public new bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;

                if (IsBusy == true)
                {
                    btnGetLocation.IsEnabled = false;
                    loadingIndicator.IsVisible = true;
                    loadingIndicator.IsRunning = true;
                }
                else
                {
                    btnGetLocation.IsEnabled = true;
                    loadingIndicator.IsVisible = false;
                    loadingIndicator.IsRunning = false;
                }
            }
        }


    }





    

    //public override async void OnAppearing()
    //{
    //    base.OnAppearing();

    //    // Show your overlay
    //    overlay.IsVisible = true;
    //    Clublistview.IsVisible = false;

    //    // Load the items into the ItemsSource
    //    await setClubs(Clublistview);

    //    // Hide the overlay
    //    overlay.IsVisible = false;
    //    Clublistview.IsVisible = true;
    //}

}
