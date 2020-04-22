using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    internal class Round
    {
        private Player winner;
        public Player Winner
        {
            get { return winner; }
            set { winner = value; }
        }

        private Choice player1Choice;
        public Choice PLayerOnesChoice
        {
            get { return player1Choice; }
            private set { player1Choice = value; }
        }
        private Choice player2Choice;
        public Choice PlayrTwosChoice 
        {
            get { return player2Choice; }
            private set { player2Choice = value; } 
        }
    }
}