using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Geolocator;
using Plugin.CurrentActivity;
using ZzAppDev.Droid;
using Android.Support.Design.Widget;
using Xamarin.Forms;
using Android.Content;

[assembly: Xamarin.Forms.Dependency(typeof(MainActivity))]
namespace ZzAppDev.Droid
{
    [Activity(Label = "ZzAppDev", Icon = "@mipmap/sincos_logo", Theme = "@style/Theme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, BackToNative
    {


        LocationDelegate locationDelegate = LocationPage.instance;
        Android.Views.View layout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);

            this.Window.AddFlags(WindowManagerFlags.Fullscreen | WindowManagerFlags.TurnScreenOn);

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            using (Bundle bundle = new Bundle())
            {
                CrossCurrentActivity.Current.Init(this, bundle);
            }

            layout = Window.DecorView.FindViewById(Android.Resource.Id.Content);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {

            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }

        public void StartNativeIntentOrActivity()
        {
            var intent = new Intent(Forms.Context, typeof(LocationActivity));
            Forms.Context.StartActivity(intent);
        }

    }
}