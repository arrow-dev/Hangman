using System;
using System.Collections.Generic;
using SQLite;
using System.IO;

namespace Hangman2016.Classes
{
	class DataManager
	{
	    private string dbPath { get; set; }
	    private SQLiteConnection db { get; set; }

        //Sets up a SQLITE database on the device and creates a table if it doesnt already exist.
        public DataManager()
	    {
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db3");
            db = new SQLiteConnection(dbPath);
	        db.CreateTable<Player>();
	    }

	    public void Insert(Player player)
	    {
	        db.Insert(player);
	    }
        
        public void Update(Player player)
        {
            db.Update(player);
        }

	    public void Delete(Player player)
	    {
	        db.Delete(player);
	    }

        //Get a list of all players from the database.
        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            var table = db.Table<Player>();
            foreach (var p in table)
            {
                players.Add(p);
            }

            return players;
        }

        //Get a list of top ten players and order descending.
        public List<Player> GetHighScoreList()
	    {
	        var hiScoreList = db.Query<Player>("SELECT * FROM Players ORDER BY HighScore DESC LIMIT 10");
	        return hiScoreList;
	    }

        //create any number of test players with random scores for testing purposes.
        public void addTestPlayers(int numberOf)
	    {
	        for (int i = 0; i < numberOf; i++)
	        {
	            var p = new Player();
	            p.Name = "test player " + i;
	            p.HighScore = new Random().Next(500);
	            db.Insert(p);
	        }
	    }
	}
}