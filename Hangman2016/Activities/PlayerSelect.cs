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

namespace Hangman2016.Activities
{
    [Activity(Label = "PlayerSelect", MainLauncher = true, Icon = "@drawable/icon")]
    public class PlayerSelect : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.playerSelect);

            // Create your application here
        }
    }
}