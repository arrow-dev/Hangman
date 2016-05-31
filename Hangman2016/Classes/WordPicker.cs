using System;
using System.Collections.Generic;

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