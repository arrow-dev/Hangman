using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Hangman2016.Classes;

namespace Hangman2016.Activities
{
    [Activity(Label = "Add Player")]
    public class AddPlayer : Activity
    {
        private EditText input;
        private Button save;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newPlayer);
            input = FindViewById<EditText>(Resource.Id.txtInput);
            save = FindViewById<Button>(Resource.Id.btnInsert);
            save.Click += Save_Click;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            //There is text create new Player object and add to database, else prompt user to enter a name.
            if (input.Text != "")
            {
                var player = new Player();
                player.Name = input.Text;
                var dbManager = new DataManager();
                dbManager.Insert(player);
                StartActivity(typeof(PlayerSelect));
            }
            else
            {
                Toast.MakeText(this, "Please enter your name!",ToastLength.Short).Show();
            }

        }
    }
}