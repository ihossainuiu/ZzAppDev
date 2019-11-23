using System;
using Android.Content;
using Xamarin.Forms;
using ZzAppDev.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(BackToNativeActivity))]
namespace ZzAppDev.Droid
{
    public class BackToNativeActivity: BackToNative
    {
        public BackToNativeActivity()
        {
        }

        public void StartNativeIntentOrActivity()
        {
            var intent = new Intent(Forms.Context, typeof(LocationActivity));
            Forms.Context.StartActivity(intent);
        }
    }
    
}
