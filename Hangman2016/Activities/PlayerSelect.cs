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
using Hangman2016.Classes;
using Newtonsoft.Json;

namespace Hangman2016.Activities
{
    [Activity(Label = "PlayerSelect", MainLauncher = true, Icon = "@drawable/icon")]
    public class PlayerSelect : Activity
    {
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

        private void HighScores_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void PlayerSelector_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            SelectedPlayer = Players[e.Position];
            Toast.MakeText(this, SelectedPlayer.Name, ToastLength.Short).Show();
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