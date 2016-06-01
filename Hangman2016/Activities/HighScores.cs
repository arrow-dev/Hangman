using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Hangman2016.Classes;

namespace Hangman2016.Activities
{
    [Activity(Label = "Top 10 Players")]
    public class HighScores : Activity
    {
        private ListView MyListView;
        private List<Player> MyList;

        //Get a list of top 10 players ordered by score, pass it to the custom listview adapter and attach the adapter to the listview.
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.highScores);
            var db = new DataManager();
            MyList = db.GetHighScoreList();
            MyListViewAdapter myAdapter = new MyListViewAdapter(this, MyList);
            MyListView = FindViewById<ListView>(Resource.Id.listHighScores);
            MyListView.Adapter = myAdapter;

        }
    }
}