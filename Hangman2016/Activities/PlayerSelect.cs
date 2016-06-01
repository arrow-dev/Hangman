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
        private Spinner PlayerSelector { get; set; }
        private List<Player> Players { get; set; }
        private Player SelectedPlayer { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Initialize views and set up data adapter for the spinner
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
            PlayerSelector = FindViewById<Spinner>(Resource.Id.spinnerPlayerSelect);
            PlayerSelector.ItemSelected += PlayerSelector_ItemSelected;
        }

        protected override void OnResume()
        {
            //This updates the data for the spinner if a Player has been added or removed.
            base.OnResume();
            Play.Enabled = false;
            Edit.Enabled = false;
            Players = GetPlayerList();
            var myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Players);
            PlayerSelector.Adapter = myAdapter;

        }

        private List<Player> GetPlayerList()
        {
            //Gets a list of Player objects currently stored in the database.
            var data = new DataManager();
            var list = data.GetAllPlayers();

            return list;
        }

        private void PlayerSelector_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Event triggered when a Player name is selected from the spinner. Sets SelectedPlayer so it can be passed to other activities and enables play and edit buttons.
            SelectedPlayer = Players[e.Position];
            Play.Enabled = true;
            Edit.Enabled = true;
        }

        private void Play_Click(object sender, EventArgs e)
        {   
            //Starts game and passes selected Player object through the intent as JSON.
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("UserProfile", JsonConvert.SerializeObject(SelectedPlayer));
            StartActivity(intent);
        }

        private void New_Click(object sender, EventArgs e)
        {   //Starts AddPlayer activity.
            StartActivity(typeof(AddPlayer));
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            //Starts EditPlayer activity and passes selected Player object through the intent as JSON.
            Intent intent = new Intent(this, typeof(EditPlayer));
            intent.PutExtra("UserProfile", JsonConvert.SerializeObject(SelectedPlayer));
            StartActivity(intent);
        }

        private void HighScores_Click(object sender, EventArgs e)
        {
            //Starts HighScores activity.
            StartActivity(typeof(HighScores));
        }
    }
}