using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Hangman2016.Classes
{
	class MyListViewAdapter : BaseAdapter<Player>
	{
	    private List<Player> mIList;
	    private Context mContext;

	    public MyListViewAdapter(Context context, List<Player> items )
	    {
	        mIList = items;
	        mContext = context;
	    }

	    public override long GetItemId(int position)
	    {
	        return position;
	    }

	    public override View GetView(int position, View convertView, ViewGroup parent)
	    {
	        View row = convertView;

	        if (row == null)
	        {
	            row = LayoutInflater.From(mContext).Inflate(Resource.Layout.highScoreRow, null, false);
	        }

	        TextView txtName = row.FindViewById<TextView>(Resource.Id.hiScoreName);
	        txtName.Text = mIList[position].Name;
	        TextView txtScore = row.FindViewById<TextView>(Resource.Id.hiScoreValue);
	        txtScore.Text = mIList[position].HighScore.ToString();
	        return row;
	    }

	    public override int Count
	    {
	        get { return mIList.Count; }
	    }

	    public override Player this[int position]
	    {
	        get { return mIList[position]; }
	    }
	}
}