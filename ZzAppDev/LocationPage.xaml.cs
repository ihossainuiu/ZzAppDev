using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace ZzAppDev
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LocationPage : ContentPage, LocationDelegate
    {
        public static LocationPage instance;
 
        public LocationPage()
        {
            InitializeComponent();
            btnGetLocation.Clicked += BtnGetLocation_Clicked;
            btnXMLReader.Clicked += BtnXMLReader_Clicked;
            instance = this;
        }


        private async void BtnGetLocation_Clicked(object sender, EventArgs e)
        {
            await CheckLocationPermissionAndRetrieveGPS();
        }

        private void BtnXMLReader_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<LocationActivityDelegate>().StartLocationActivity();
        }

        private async Task CheckLocationPermissionAndRetrieveGPS()
        {

            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            if (permissionStatus == PermissionStatus.Granted)
            {
                await RetrieveLocation();
                return;
            }
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
        }

        public async Task RetrieveLocation()
        {

            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 20;

            IsBusy = true;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5), null, true);
            IsBusy = false;

            txtLat.Text = position.Latitude.ToString();
            txtLong.Text = position.Longitude.ToString();

        }

        async Task LocationDelegate.GetGPSLocationAsync()
        {
            await RetrieveLocation();
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


}
