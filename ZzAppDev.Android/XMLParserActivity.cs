using System;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.App;
using Android.Widget;
using Android.Content.PM;
using Android.Content.Res;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace ZzAppDev.Droid
{
    [Activity(Label = "XML Parser Activity", Icon = "@mipmap/icon", Theme = "@style/Theme.AppCompat.Light", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LocationXMLActiity: AppCompatActivity, AsyncResponse
    {

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        LocationsXML mLocationsXML;
        LocationXMLAdapter mAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            XDocument xmlDoc = XDocument.Load(this.Assets.Open("LocationData.xml"));

            IEnumerable<City> _cities = from s in xmlDoc.Descendants("city")
                                        select new City
                                        {
                                            city = s.Element("name").Value,
                                            country = s.Element("country").Value,
                                        };

            mLocationsXML = new LocationsXML();
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Main);
            mRecycleView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new LocationXMLAdapter(mLocationsXML);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);

            //mLocationsXML.locations = _cities.ToList();
            //mAdapter.NotifyDataSetChanged();

            new RetrieveXMLData(this, mAdapter, this.Assets, this).Execute();
            //new MyTask().Execute();

        }

        private void MAdapter_ItemClick(object sender, int e)
        {            
            Toast.MakeText(this, "Location: City: " + mLocationsXML[e].city + "; Country: " + mLocationsXML[e].country, ToastLength.Short).Show();
        }

        public void processFinish(List<City> result)
        {
            mLocationsXML.locations = result;
            mAdapter.NotifyDataSetChanged();
            Toast.MakeText(this, "successful", ToastLength.Long).Show();
        }

    }

}
