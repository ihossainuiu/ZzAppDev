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
using Android.Views;

namespace ZzAppDev.Droid
{
    [Activity(Label = "XML Parser Activity", Icon = "@mipmap/icon", Theme = "@style/ThemeNoActionBar", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class XMLParserActivity : AppCompatActivity, AsyncResponse
    {

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        LocationsObj mLocationsObj;
        LocationXMLAdapter mAdapter;
        ProgressDialog mDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;
            //ActionBar.SetHomeButtonEnabled(true);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);


            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "XML RecyclerView Activity";

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            mLocationsObj = new LocationsObj();
            // Set our view from the "main" layout resource  
            mRecycleView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new LocationXMLAdapter(mLocationsObj);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);

            mDialog = new ProgressDialog(this);
            mDialog.SetMessage("Please wait...");
            mDialog.SetCancelable(false);


            retrieveData();
            

        }

        private void retrieveData()
        {
            new RetrieveXMLData(this, mAdapter, this.Assets, this).Execute();
            mDialog.Show();
        }

        private void MAdapter_ItemClick(object sender, int e)
        {            
            Toast.MakeText(this, "Location: City: " + mLocationsObj[e].city + "; Country: " + mLocationsObj[e].country, ToastLength.Short).Show();
        }

        public void processFinish(List<City> result)
        {
            mLocationsObj.locations = result;
            mAdapter.NotifyDataSetChanged();

            if (mDialog != null)
            {
                if (mDialog.IsShowing)
                {
                    mDialog.Dismiss();
                }
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


    }

}
