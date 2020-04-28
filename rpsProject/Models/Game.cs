//using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    public class Game
    {
        public Game() { }
        public Game (Player p1, Player p2)
        {
            this.PlayerOne = p1;
            this.PlayerTwo = p2;
        }

        public int GameId { get; set; }
        private List<Round> rounds = new List<Round>();
        public List<Round> Rounds
        {
            get { return rounds; }
            set { rounds = value; }
        }

        private Player p1;
        public Player PlayerOne
        {
            get { return p1; }
            set { p1 = value; }
        }

        private Player p2;
        public Player PlayerTwo
        {
            get { return p2; }
            set { p2 = value; }
        }
    }
}