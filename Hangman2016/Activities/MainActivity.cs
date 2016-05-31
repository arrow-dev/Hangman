﻿using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text.Method;
using Hangman2016.Classes;
using Newtonsoft.Json;

namespace Hangman2016
{
    [Activity(Label = "Hangman")]
    public class MainActivity : Activity
    {
        private Player PlayerProfile { get; set; }
        private ImageView DisplayImg { get; set; }
        private LinearLayout DisplayWord{ get; set; }
        private Button MyButton { get; set; }
        private EditText Input{ get; set; }
        private Button BtnNewGame { get; set; }
        private List<string> WordList { get; set; }
        private string Word { get; set; }
        private Animator MyAnimator { get; set; }
        private List<Button> KeyboardButtons { get; set; }
        private int WordScore { get; set; }
        private int LossPoints { get; set; }
        private TextView Score { get; set; }
        private DataManager myDataManager;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            PlayerProfile = JsonConvert.DeserializeObject<Player>(Intent.GetStringExtra("UserProfile"));
            myDataManager =new DataManager();
            Score = FindViewById<TextView>(Resource.Id.Score);
            Score.Text = "Player Score: " + PlayerProfile.HighScore;
            DisplayImg = FindViewById<ImageView>(Resource.Id.imageViewDisplayImg);
            DisplayWord = FindViewById<LinearLayout>(Resource.Id.linearLayoutDislplayWord);
            BtnNewGame = FindViewById<Button>(Resource.Id.buttonNewGame);
            BtnNewGame.Click += BtnNewGame_Click;
            WordList = new List<string>();
            Stream myStream = Assets.Open("Words.txt");
            StreamReader myReader = new StreamReader(myStream);
            while (!myReader.EndOfStream)
            {
                WordList.Add(myReader.ReadLine());
            }
            KeyboardButtons = new List<Button>();
            KeyboardSetup();
            NewGame();
        }

        private void NewGame()
        {
            BtnNewGame.Visibility = ViewStates.Invisible;
            MyAnimator = new Animator();
            DisplayImg.SetImageResource(MyAnimator.GetNextResource());
            WordPicker myWordPicker = new WordPicker(WordList);
            Word = myWordPicker.GetRandomWord();
            Score s = new Score();
            WordScore = s.GetScore(Word);
            LossPoints = 10;
            DisplayWord.RemoveAllViews();
            foreach (var letter in Word)
            {
                DisplayWord.AddView(new TextView(this) {Text = letter.ToString(), TextSize = 50, TransformationMethod = new PasswordTransformationMethod()});
            }
            foreach (Button key in KeyboardButtons)
            {
                key.Visibility=ViewStates.Visible;
                key.Enabled = true;
            }
        }

        

        private void GameFinished()
        {
            BtnNewGame.Visibility = ViewStates.Visible;
            BtnNewGame.Background = new ColorDrawable(Color.Red);
            foreach (Button key in KeyboardButtons)
            {
                key.Visibility = ViewStates.Gone;
            }
            myDataManager.Update(PlayerProfile);
            Score.Text = "Player Score: " + PlayerProfile.HighScore;
        }

        private bool CheckIfComplete()
        {
            bool complete = true;
            for (int i = 0; i < DisplayWord.ChildCount; i++)
            {
                TextView myTextView = (TextView) DisplayWord.GetChildAt(i);
                if (myTextView.TransformationMethod != null)
                {
                    complete = false;
                }
            }

            return complete;
        }

        private void KeyboardSetup()
        {
            LinearLayout Keyboard = FindViewById<LinearLayout>(Resource.Id.linearLayoutKeyboard);
            for (int i = 0; i < Keyboard.ChildCount; i++)
            {
                var r = Keyboard.GetChildAt(i);
                if (r is LinearLayout)
                {
                    LinearLayout Row = (LinearLayout) r;
                    for (int j = 0; j < Row.ChildCount; j++)
                    {
                        var l = Row.GetChildAt(j);
                        if (l is Button)
                        {
                            l.Click += Letter_Click;
                            KeyboardButtons.Add((Button)l);
                        }
                    }
                }
            }
        }

        private void Letter_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button) sender;
            String Letter = clickedButton.Text;
            if (Letter != "")
            {
                clickedButton.Enabled = false;
                bool correctGuess = false;
                for (int i = 0; i < DisplayWord.ChildCount; i++)
                {
                    TextView myTextView = (TextView)DisplayWord.GetChildAt(i);
                    if (myTextView.Text == Letter.ToLower())
                    {
                        myTextView.TransformationMethod = null;
                        correctGuess = true;
                    }
                }
                if (correctGuess)
                {
                    WordScore += 2;
                    if (CheckIfComplete())
                    {
                        Toast.MakeText(this, "you win! +" + WordScore + "points!", ToastLength.Long).Show();
                        PlayerProfile.HighScore += WordScore;
                        GameFinished();
                    }
                }
                else
                {
                    WordScore -= 1;
                    DisplayImg.SetImageResource(MyAnimator.GetNextResource());
                    if (MyAnimator.EndOfResources)
                    {
                        Toast.MakeText(this, "you lose! -" + LossPoints + "\n the word was " + Word, ToastLength.Long).Show();
                        PlayerProfile.HighScore -= LossPoints;
                        GameFinished();
                    }
                }
            }
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}

