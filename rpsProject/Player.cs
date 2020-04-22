using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    internal class Player
    {
        //player name variable holder
        String playerName;
        public string Name
        {
            get { return playerName; }
        }
        //player score variable holder
        int playerScore;
        public int Score
        {
            get { return playerScore; }
        }
        public Player(string name)
        {
            this.playerName = name;
            playerScore = 0;
        }

        //method for increasing player's score
        public void IncreaseScore()
        {
            playerScore++;
        }
    }
}
