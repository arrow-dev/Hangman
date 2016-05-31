using SQLite;

namespace Hangman2016.Classes
{
    [Table("Players")]
    class Player
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int HighScore { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}