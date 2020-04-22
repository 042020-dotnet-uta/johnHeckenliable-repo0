using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    internal class Round
    {
        private Player winner = null;
        public Player Winner
        {
            get { return winner; }
            set { winner = value; }
        }

        private Choice player1Choice;
        public Choice PLayerOnesChoice
        {
            get { return player1Choice; }
            set { player1Choice = value; }
        }
        private Choice player2Choice;
        public Choice PlayerTwosChoice 
        {
            get { return player2Choice; }
            set { player2Choice = value; } 
        }
    }
}