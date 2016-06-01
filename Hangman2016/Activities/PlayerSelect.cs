using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Hangman2016.Classes;
using Newtonsoft.Json;

namespace Hangman2016.Activities
{
    //Set flags to make this the MainLauncher activity and prevent multiple intances.
    [Activity(Label = "Hangman", MainLauncher = true, Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleTask)]
    public class PlayerSelect : Activity
    {
        private Button Play { get; set; }
        private Button New { get; set; }
        private Button Edit { get; set; }
        private Button HighScores { get; set; }
        private Button AddTestPlayers { get; set; }
        private Spinner PlayerSelector { get; set; }
        private List<Player> Players { get; set; }
        private Player SelectedPlayer { get; set; }

        //Initialize views and set up data adapter for the spinner
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.playerSelect);
            Play = FindViewById<Button>(Resource.Id.btnPlay);
            Play.Click += Play_Click;
            New = FindViewById<Button>(Resource.Id.btnNew);
            New.Click += New_Click;
            Edit = FindViewById<Button>(Resource.Id.btnEdit);
            Edit.Click += Edit_Click;
            HighScores = FindViewById<Button>(Resource.Id.btnHiScores);
            HighScores.Click += HighScores_Click;
            AddTestPlayers = FindViewById<Button>(Resource.Id.btnTestPlayers);
            AddTestPlayers.Click += AddTestPlayers_Click;
            PlayerSelector = FindViewById<Spinner>(Resource.Id.spinnerPlayerSelect);
            PlayerSelector.ItemSelected += PlayerSelector_ItemSelected;
        }

        //Adds 20 test players with random scores into the db;
        private void AddTestPlayers_Click(object sender, EventArgs e)
        {
            var db = new DataManager();
            db.addTestPlayers(20);
            OnResume();
        }

        //This updates the data for the spinner if a Player has been added or removed.
        protected override void OnResume()
        {
            base.OnResume();
            Play.Enabled = false;
            Edit.Enabled = false;
            Players = GetPlayerList();
            var myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Players);
            PlayerSelector.Adapter = myAdapter;

        }

        //Gets a list of Player objects currently stored in the database.
        private List<Player> GetPlayerList()
        {
            var data = new DataManager();
            var list = data.GetAllPlayers();

            return list;
        }

        //Event triggered when a Player name is selected from the spinner. Sets SelectedPlayer so it can be passed to other activities and enables play and edit buttons.
        private void PlayerSelector_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            SelectedPlayer = Players[e.Position];
            Play.Enabled = true;
            Edit.Enabled = true;
        }

        //Starts game and passes selected Player object through the intent as JSON.
        private void Play_Click(object sender, EventArgs e)
        {   
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("UserProfile", JsonConvert.SerializeObject(SelectedPlayer));
            StartActivity(intent);
        }

        //Starts AddPlayer activity.
        private void New_Click(object sender, EventArgs e)
        {   
            StartActivity(typeof(AddPlayer));
        }

        //Starts EditPlayer activity and passes selected Player object through the intent as JSON.
        private void Edit_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(EditPlayer));
            intent.PutExtra("UserProfile", JsonConvert.SerializeObject(SelectedPlayer));
            StartActivity(intent);
        }

        //Starts HighScores activity.
        private void HighScores_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HighScores));
        }
    }
}