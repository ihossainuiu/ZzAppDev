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
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnNextActivity.Clicked += BtnNextActivity_Clicked;

        }

        private async void BtnNextActivity_Clicked(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new LocationPage());

        }

    }
}
