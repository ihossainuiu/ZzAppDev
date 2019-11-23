using Android.Content;
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

    public class City
    {
        public string country { get; set; }
        public string city { get; set; }
    }

    public class LocationsObj
    {

        public List<City> locations = new List<City>();

        public LocationsObj()
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

    public class LocationRViewHolder : RecyclerView.ViewHolder
    {
        public TextView CityTV { get; set; }
        public TextView CountryTV { get; set; }

        public LocationRViewHolder(View itemview, Action<int> listener) : base(itemview)
        {
            CityTV = itemview.FindViewById<TextView>(Resource.Id.tvCity);
            CountryTV = itemview.FindViewById<TextView>(Resource.Id.tvCountry);

            itemview.Click += (sender, e) => listener(base.Position);

        }
    }


}