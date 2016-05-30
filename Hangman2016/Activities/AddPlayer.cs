using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
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
            // Create your application here
        }

        private void Save_Click(object sender, EventArgs e)
        {
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