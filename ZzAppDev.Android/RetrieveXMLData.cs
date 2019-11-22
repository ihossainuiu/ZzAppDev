using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Widget;

namespace ZzAppDev.Droid
{
    public class RetrieveXMLData : AsyncTask<string, string, List<City>>
    {

        List<City> cities;
        Context mContext;
        LocationXMLAdapter mAdapter;
        AssetManager mAssets;

        public AsyncResponse resDelegate = null;

        public RetrieveXMLData(Context context, LocationXMLAdapter adapter, AssetManager assets)
        {
            mContext = context;
            mAdapter = adapter;
            mAssets = assets;
        }

        public RetrieveXMLData(Context context, LocationXMLAdapter adapter, AssetManager assets, AsyncResponse asyncResponse)
        {
            mContext = context;
            mAdapter = adapter;
            mAssets = assets;
            resDelegate = asyncResponse;
        }

        protected override void OnPreExecute()
        {
            //AndroidHUD.AndHUD.Shared.ShowImage(mContext, Resource.Drawable.load2, "Getting Posts...");
        }

        protected override List<City> RunInBackground(params string[] @params)
        {
            List<City> rawData = null;

            XDocument xmlDoc = XDocument.Load(mAssets.Open("LocationData.xml"));


            IEnumerable<City> _cities = from s in xmlDoc.Descendants("city")
                                        select new City
                                        {
                                            city = s.Element("name").Value,
                                            country = s.Element("country").Value,
                                        };

            rawData = _cities.ToList();

            return rawData;
        }

        protected override void OnPostExecute(Java.Lang.Object result)
        {
            base.OnPostExecute(result);
        }

        protected override void OnPostExecute(List<City> result)
        {
            base.OnPostExecute(result);

            if (resDelegate != null)
            {
                resDelegate.processFinish(result);
            }
        }

    }
}

