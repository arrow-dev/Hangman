using System;
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
        private DataManager MyDataManager { get; set; }


        //Initialize views, set up list of words from file, set up keyboard and start game.
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            PlayerProfile = JsonConvert.DeserializeObject<Player>(Intent.GetStringExtra("UserProfile"));
            MyDataManager =new DataManager();
            Score = FindViewById<TextView>(Resource.Id.Score);
            Score.Text = "Player Score: " + PlayerProfile.HighScore;
            DisplayImg = FindViewById<ImageView>(Resource.Id.imageViewDisplayImg);
            DisplayWord = FindViewById<LinearLayout>(Resource.Id.linearLayoutDislplayWord);
            BtnNewGame = FindViewById<Button>(Resource.Id.buttonNewGame);
            BtnNewGame.Click += BtnNewGame_Click;
            WordList = new List<string>();
            LossPoints = 10;
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

        //MAIN GAME LOGIC:
        private void Letter_Click(object sender, EventArgs e)
        {
            //When a letter button is clicked create a reference to it by casting the sender object to a button, then get the text from the button and disable it.
            Button clickedButton = (Button)sender;
            String Letter = clickedButton.Text;
            clickedButton.Enabled = false;

            //If letter matches any the the DisplayWord's TextView's text property then remove the password mask and set correctGuess bool to true.
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
            //If it was a correct guess then add points and check if the word is complete, if so then the player wins.
            if (correctGuess)
            {
                WordScore += 2;
                if (CheckIfComplete())
                {
                    //WIN CONDITION alert the player, update profile score and call GameFinished method.
                    Toast.MakeText(this, "YOU WIN! +" + WordScore + "points!", ToastLength.Long).Show();
                    PlayerProfile.HighScore += WordScore;
                    GameFinished();
                }
            }
            else
            //Guess was wrong, decrement points, call the animator to get next image, if animatator is out of images the player loses.
            {
                WordScore -= 1;
                DisplayImg.SetImageResource(MyAnimator.GetNextResource());
                if (MyAnimator.EndOfResources)
                {
                    //LOSE CONDITION alert the player, update profile score and call GameFinished method.
                    Toast.MakeText(this, "YOU LOSE! -" + LossPoints + "points!\nThe word was " + Word, ToastLength.Long).Show();
                    PlayerProfile.HighScore -= LossPoints;
                    GameFinished();
                }
            }

        }

        //This method resets the Animator and then calls it to get the starting image, gets a new word and word score, sets up views for new word and resets the keyboard buttons.
        private void NewGame()
        {
            BtnNewGame.Visibility = ViewStates.Invisible;
            MyAnimator = new Animator();
            DisplayImg.SetImageResource(MyAnimator.GetNextResource());
            WordPicker myWordPicker = new WordPicker(WordList);
            Word = myWordPicker.GetRandomWord();
            Score s = new Score();
            WordScore = s.GetScore(Word);
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

        //When game is finished enable the New Game button, hide the keyboard, update the database and update the score label.
        private void GameFinished()
        {
            BtnNewGame.Visibility = ViewStates.Visible;
            BtnNewGame.Background = new ColorDrawable(Color.Red);
            foreach (Button key in KeyboardButtons)
            {
                key.Visibility = ViewStates.Gone;
            }
            MyDataManager.Update(PlayerProfile);
            Score.Text = "Player Score: " + PlayerProfile.HighScore;
        }

        //Check if any Text Views in the DisplayWord linear layoout still have their text masked by the PasswordWordTransformation method. If any letters are still hidden word is not complete.
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

        //Loop through each KeyboardRow linear layout and for each button in the row assign a click event handler and add the button to a List of keyboard buttons.
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

        //Click handler for new game button.
        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}

