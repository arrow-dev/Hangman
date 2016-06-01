namespace Hangman2016.Classes
{

    //SCRABBLE SCORING
    class Score
    {
        public Score()
        {
        }

        //Pass in a word and get the score.
        public int GetScore(string word)
        {
            int score = 0;
            foreach (var letter in word)
            {
                switch (letter)
                {
                    case 'a':
                    {
                        score += 1;
                        break;
                    }
                    case 'b':
                        {
                            score += 3;
                            break;
                        }
                    case 'c':
                        {
                            score += 3;
                            break;
                        }
                    case 'd':
                        {
                            score += 2;
                            break;
                        }
                    case 'e':
                        {
                            score += 1;
                            break;
                        }
                    case 'f':
                        {
                            score += 4;
                            break;
                        }
                    case 'g':
                        {
                            score += 2;
                            break;
                        }
                    case 'h':
                        {
                            score += 4;
                            break;
                        }
                    case 'i':
                        {
                            score += 1;
                            break;
                        }
                    case 'j':
                        {
                            score += 8;
                            break;
                        }
                    case 'k':
                        {
                            score += 5;
                            break;
                        }
                    case 'l':
                        {
                            score += 1;
                            break;
                        }
                    case 'm':
                        {
                            score += 3;
                            break;
                        }
                    case 'n':
                        {
                            score += 1;
                            break;
                        }
                    case 'o':
                        {
                            score += 1;
                            break;
                        }
                    case 'p':
                        {
                            score += 3;
                            break;
                        }
                    case 'q':
                        {
                            score += 10;
                            break;
                        }
                    case 'r':
                        {
                            score += 1;
                            break;
                        }
                    case 's':
                        {
                            score += 1;
                            break;
                        }
                    case 't':
                        {
                            score += 1;
                            break;
                        }
                    case 'u':
                        {
                            score += 1;
                            break;
                        }
                    case 'v':
                        {
                            score += 4;
                            break;
                        }
                    case 'w':
                        {
                            score += 4;
                            break;
                        }
                    case 'x':
                        {
                            score += 8;
                            break;
                        }
                    case 'y':
                        {
                            score += 4;
                            break;
                        }
                    case 'z':
                        {
                            score += 10;
                            break;
                        }
                }
            }

            return score;
        }
    }
}