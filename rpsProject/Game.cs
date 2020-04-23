using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    class Game
    {
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
            private set { p1 = value; }
        }

        private Player p2;
        public Player PlayerTwo
        {
            get { return p2; }
            private set { p2 = value; }
        }

        internal void StartGame()
        {
            //Set up the players info
            GetPlayerNames();
            var p1Score = 0;
            var p2Score = 0;
            do
            {
                //run main gameplay method
                var round = PlayRound();

                if (round.Winner == p1)
                    p1Score++;
                else
                    p2Score++;

                rounds.Add(round);
                //Display round info
                DisplayRoundInfo(round);
            } while ((p1Score < 2) && (p2Score < 2));

            DisplayResults(p1Score, p2Score);
        }
        private void GetPlayerNames()
        {
            //set player 1 name (readLine)
            System.Console.Write("Player 1, please input your name:");
            PlayerOne = new Player(Console.ReadLine());
            //set player 2 name (readLine)
            System.Console.Write("Player 2, please input your name:");
            PlayerTwo = new Player(Console.ReadLine());
        }
        private Round PlayRound()
        {
            var round = new Round();

            var rnd = new Random();
            round.PLayerOnesChoice = (Choice)rnd.Next(3);
            round.PlayerTwosChoice = (Choice)rnd.Next(3);

            //Algorithm and switch statements taken from Ash, Kuang, ??? demo code
            var win = round.PLayerOnesChoice - round.PlayerTwosChoice + 2;

            //Switch statement refactored to eliminate redundant code
            switch (win)
            {
                case 0:
                case 3:
                    round.Winner = p1;
                    break;
                case 1:
                case 4:
                    round.Winner = p2;
                    break;
                default:
                    break;
            }
            return round;
        }

        private void DisplayResults(int p1Score, int p2Score)
        {
            var tieCount = this.rounds.Count - (p1Score + p2Score);
            if (p1Score >= 2)
            {
                //write to console "[player1] wins [player1.score]-[player2.score] with [tie count] ties"
                System.Console.WriteLine($"{p1.Name} wins {p1Score}-{p2Score} with {tieCount} tie(s).");
            }
            else
            {   //} else if player 2 score more than 2 {
                //write to console "[player2] wins [player2.score]-[player1.score] with [tie count] ties"
                System.Console.WriteLine($"{p2.Name} wins {p2Score}-{p1Score} with {tieCount} tie(s).");
            }
        }

        private void DisplayRoundInfo(Round round)
        {
            //check winner ([IF] there is a tie)
            if (round.Winner == null)
            {
                //write there was a tie
                System.Console.WriteLine($"Round {rounds.Count}: {p1.Name} and {p2.Name} had {round.PlayerTwosChoice} -- Tie.");
            }
            else
            {
                //round # - [player1] had [value], [player2] had [value] - [winner] won
                System.Console.WriteLine($"Round {rounds.Count}: {p1.Name} had {round.PLayerOnesChoice}, {p2.Name} had {round.PlayerTwosChoice} -- {round.Winner.Name} wins.");
            }
        }
    }
}