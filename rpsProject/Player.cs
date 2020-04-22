using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    internal class Player
    {
        public Player(string name)
        {
            this.playerName = name;
        }

        //player name variable holder
        private string playerName;
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