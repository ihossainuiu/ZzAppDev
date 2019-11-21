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

namespace ZzAppDev.Droid
{
    [Activity(Label = "ZzAppDev", Icon = "@mipmap/icon", Theme = "@style/Theme.Splash", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LocationActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            this.Window.ClearFlags(WindowManagerFlags.Fullscreen);

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


            using (Bundle bundle = new Bundle())
            {
                CrossCurrentActivity.Current.Init(this, bundle);
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {

            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


        }



        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

    }
}