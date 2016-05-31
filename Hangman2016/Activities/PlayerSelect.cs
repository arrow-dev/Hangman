using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Hangman2016.Classes;
using Newtonsoft.Json;

namespace Hangman2016.Activities
{
    [Activity(Label = "Player Select", MainLauncher = true, Icon = "@drawable/icon")]
    public class PlayerSelect : Activity
    {
        private Button Play;
        private Button New;
        private Button Edit;
        private Button HighScores;
        private Spinner PlayerSelector;
        private List<Player> Players { get; set; }
        private ArrayAdapter MyAdapter { get; set; }
        private Player SelectedPlayer { get; set; }

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
            PlayerSelector = FindViewById<Spinner>(Resource.Id.spinnerPlayerSelect);
            PlayerSelector.ItemSelected += PlayerSelector_ItemSelected;
            // Create your application here
            Players = GetPlayerList();
            MyAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Players);
            PlayerSelector.Adapter = MyAdapter;
        }

        private void Play_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("UserProfile", JsonConvert.SerializeObject(SelectedPlayer));
            StartActivity(intent);
        }

        private void HighScores_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HighScores));
        }

        private void PlayerSelector_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            SelectedPlayer = Players[e.Position];
            Play.Enabled = true;
        }

        private List<Player> GetPlayerList()
        {
            var data = new DataManager();
            var list = data.GetAllPlayers();

            return list;
        }

        private void New_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddPlayer));
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(EditPlayer));
            intent.PutExtra("UserProfile", JsonConvert.SerializeObject(SelectedPlayer));
            StartActivity(intent);
        }
    }
}