using System;
using Xamarin.Forms;

namespace ZzAppDev
{

    public interface BackToNative
    {
        void StartNativeIntentOrActivity();
    }

    public interface LocationActivityDelegate
    {
        void StartLocationActivity();
    }
}
