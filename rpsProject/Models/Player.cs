using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    public class Player
    {
        public Player() { }
        public Player(string name)
        {
            this.playerName = name;
        }

        public int PlayerId { get; set; }

        //player name variable holder
        private readonly string playerName;
        public string Name
        {
            get { return playerName; }
        }

        private int wins;
        public int Wins
        {
            get { return wins; }
            private set { wins = value; }
        }

        private int losses;
        public int Losses
        {
            get { return losses; }
            private set { losses = value; }
        }

        public void IncrementWins()
        {
            Wins++;
        }
        public void IncrementLosses()
        {
            Losses++;
        }
    }
}