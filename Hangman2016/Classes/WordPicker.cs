using System;
using System.Collections.Generic;

namespace Hangman2016
{
    //Pass in a list of words to initialize.
    public class WordPicker
    {
        private List<string> Wordlist { get; set; }

        public WordPicker(List<string> list )
        {
            Wordlist = list;
        }

        //Call this method to get a random word from the list.
        public string GetRandomWord()
        {
            int randomIndex = new Random().Next(Wordlist.Count);
            string word = Wordlist[randomIndex];

            return word;
        }
    }
}