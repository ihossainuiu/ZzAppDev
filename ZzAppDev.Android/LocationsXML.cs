﻿using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZzAppDev.Droid
{


    public class MyTask : AsyncTask<string, int, int>
    {
        public MyTask()
        {

        }

        protected override int RunInBackground(params string[] @params)
        {
            return 4;
        }

        protected override void OnPostExecute(int result)
        {
            System.Console.WriteLine("On post execute");
            base.OnPostExecute(result);
        }
    }



    public class City
    {
        public string country { get; set; }
        public string city { get; set; }
    }
    public class LocationsXML
    {

        public List<City> locations = new List<City>();

        //new Location() { mPhotoID = Resource.Drawable.Android3, mCaption = "Ahsan 3"},
        //    new Location() { mPhotoID = Resource.Drawable.Android4, mCaption = "Ahsan 4"},
        //    new Location() { mPhotoID = Resource.Drawable.Android5, mCaption = "Ahsan 5"},
        //    new Location() { mPhotoID = Resource.Drawable.Android1, mCaption = "Ahsan 6"},

        public LocationsXML()
        {
        }

        public int count
        {
            get
            {
                return locations.Count;
            }
        }

        public City this[int i]
        {
            get { return locations[i]; }
        }

    }

    public class LocationXMLViewHolder : RecyclerView.ViewHolder
    {
        public TextView CityTV { get; set; }
        public TextView CountryTV { get; set; }

        public LocationXMLViewHolder(View itemview, Action<int> listener) : base(itemview)
        {
            CityTV = itemview.FindViewById<TextView>(Resource.Id.tvCity);
            CountryTV = itemview.FindViewById<TextView>(Resource.Id.tvCountry);

            itemview.Click += (sender, e) => listener(base.Position);

        }
    }


}