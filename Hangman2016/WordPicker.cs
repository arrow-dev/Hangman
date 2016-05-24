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
using Java.Lang;

namespace Hangman2016
{
    public class WordPicker
    {
        private List<string> Wordlist { get; set; }

        public WordPicker(List<string> list )
        {
            Wordlist = list;
        }

        public string GetRandomWord()
        {
            int randomIndex = new Random().Next(Wordlist.Count);
            string word = Wordlist[randomIndex];

            return word;
        }
    }
}