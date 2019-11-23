using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Geolocator;
using Plugin.CurrentActivity;
using Android.Content.Res;
using Xamarin.Forms.Platform.Android;
using System.IO;
using ZzAppDev.Droid;
using Android.Support.V7.App;
using Android.Content;
using Xamarin.Forms;
using Android.Support.Design.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(LocationActivity))]
namespace ZzAppDev.Droid
{
    [Activity(Label = "ZzAppDev", Icon = "@mipmap/icon", Theme = "@style/ThemeNoActionBar", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LocationActivity : AppCompatActivity, LocationActivityDelegate
    {

        Android.Views.View layout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            this.Window.ClearFlags(WindowManagerFlags.Fullscreen);

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            using (Bundle bundle = new Bundle())
            {
                CrossCurrentActivity.Current.Init(this, bundle);
            }



            SetContentView(Resource.Layout.GPSLocation);

            layout = Window.DecorView.FindViewById(Android.Resource.Id.Content);


            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "GPS Location Finder";

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));
            Android.Support.V4.App.Fragment locationPage = new LocationPage().CreateSupportFragment(this);
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.fragment_frame_layout, locationPage)
                .Commit();

        }

        public const int RequestLocationId = 25;


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {

            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


            switch (requestCode)
            {
                case RequestLocationId:
                    {

                        if (grantResults[0] == Permission.Granted)
                        {
                            //Permission granted
                            var snack = Snackbar.Make(layout, "Location permission is available, getting lat/long.", Snackbar.LengthLong);
                            snack.Show();

                            _ = LocationPage.instance.RetrieveLocation();

                        }
                        else
                        {
                            //Permission Denied :(
                            //Disabling location functionality
                            var snack = Snackbar.Make(layout, "Location permission is denied. Please Go To Settings and allow this permission First!", Snackbar.LengthLong);
                            snack.Show();
                        }
                    }
                    break;
            }

        }




        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }


        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public void StartLocationActivity()
        {
            var intent = new Intent(Forms.Context, typeof(XMLParserActivity));
            Forms.Context.StartActivity(intent);
        }
    }
}