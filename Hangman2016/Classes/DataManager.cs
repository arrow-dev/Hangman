using System;
using System.Collections.Generic;
using SQLite;
using System.IO;
using Javax.Crypto.Interfaces;

namespace Hangman2016.Classes
{
	class DataManager
	{
	    private string dbPath { get; set; }
	    private SQLiteConnection db { get; set; }

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

	    public List<Player> GetHighScoreList()
	    {
	        var hiScoreList = db.Query<Player>("SELECT * FROM Players ORDER BY HighScore DESC LIMIT 10");
	        return hiScoreList;
	    }
	}
}