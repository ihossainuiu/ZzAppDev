using Android.Support.V7.Widget;  
using Android.Views;  
using System;  
namespace ZzAppDev.Droid
{
    public class LocationXMLAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public LocationsObj mLocationsXML;
        public LocationXMLAdapter(LocationsObj locations)
        {
            mLocationsXML = locations;
        }
        public override int ItemCount
        {
            get { return mLocationsXML.count; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            LocationRViewHolder vh = holder as LocationRViewHolder;
            vh.CityTV.Text = mLocationsXML[position].city.ToString();
            vh.CountryTV.Text = mLocationsXML[position].country.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LocationCard, parent, false);
            LocationRViewHolder vh = new LocationRViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
}