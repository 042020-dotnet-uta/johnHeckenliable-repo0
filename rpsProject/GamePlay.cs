using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpsProject
{
    class GamePlay
    {
        //install the logger for a console app.
        private readonly ILogger _logger;
        private Game _game;
        public GamePlay(ILogger<GamePlay> logger)
        {
            _logger = logger;
            _game = new Game();
        }

        internal void StartGame()
        {
            _logger.LogInformation("Game Starting!");

            //Set up the players info
            GetPlayerNames();
            var p1Score = 0;
            var p2Score = 0;
            do
            {
                Console.WriteLine();
                _logger.LogInformation($"Round {_game.Rounds.Count + 1} Starting!");

                //run main gameplay method
                var round = PlayRound();

                if (round.Winner == _game.PlayerOne)
                    p1Score++;
                else
                    p2Score++;

                _game.Rounds.Add(round);
                //Display round info
                DisplayRoundInfo(round);
                _logger.LogInformation($"Round {_game.Rounds.Count} Ended!");

            } while ((p1Score < 2) && (p2Score < 2));

            DisplayResults(p1Score, p2Score);
            EndGame(p1Score >= 2 ? _game.PlayerOne : _game.PlayerTwo);
            _logger.LogInformation($"Game Ended!");
        }
        private void GetPlayerNames()
        {
            _logger.LogInformation($"Getting player names!");

            //set player 1 name (readLine)
            System.Console.Write("Player 1, please input your name:");
            _game.PlayerOne = new Player(Console.ReadLine());
            //set player 2 name (readLine)
            System.Console.Write("Player 2, please input your name:");
            _game.PlayerTwo = new Player(Console.ReadLine());
        }
        private Round PlayRound()
        {
            var round = new Round();
            AssignPlayerChoices(round);

            //Algorithm and switch statements taken from Ash, Kuang, Dave demo code
            var win = round.PLayerOnesChoice - round.PlayerTwosChoice + 2;

            //Switch statement refactored to eliminate redundant code
            switch (win)
            {
                case 0:
                case 3:
                    round.Winner = _game.PlayerOne;
                    break;
                case 1:
                case 4:
                    round.Winner = _game.PlayerTwo;
                    break;
                default:
                    break;
            }
            return round;
        }

        private void AssignPlayerChoices(Round round)
        {
            _logger.LogInformation($"Assigning player choices");

            var rnd = new Random();
            round.PLayerOnesChoice = (Choice)rnd.Next(3);
            round.PlayerTwosChoice = (Choice)rnd.Next(3);
        }

        private void EndGame(Player winner)
        {
            if (winner == _game.PlayerOne)
            {
                _game.PlayerOne.IncrementWins();
                _game.PlayerTwo.IncrementLosses();
            }
            else
            {
                _game.PlayerTwo.IncrementWins();
                _game.PlayerOne.IncrementLosses();
            }
        }

        private void DisplayResults(int p1Score, int p2Score)
        {
            var tieCount = _game.Rounds.Count - (p1Score + p2Score);
            if (p1Score >= 2)
            {
                //write to console "[player1] wins [player1.score]-[player2.score] with [tie count] ties"
                System.Console.WriteLine($"{_game.PlayerOne.Name} wins {p1Score}-{p2Score} with {tieCount} tie(s).");
            }
            else
            {   //} else if player 2 score more than 2 {
                //write to console "[player2] wins [player2.score]-[player1.score] with [tie count] ties"
                System.Console.WriteLine($"{_game.PlayerTwo.Name} wins {p2Score}-{p1Score} with {tieCount} tie(s).");
            }
        }

        private void DisplayRoundInfo(Round round)
        {
            //check winner ([IF] there is a tie)
            if (round.Winner == null)
            {
                //write there was a tie
                System.Console.WriteLine($"Round {_game.Rounds.Count}: {_game.PlayerOne.Name} and {_game.PlayerTwo.Name} had {round.PlayerTwosChoice} -- Tie.");
            }
            else
            {
                //round # - [player1] had [value], [player2] had [value] - [winner] won
                System.Console.WriteLine($"Round {_game.Rounds.Count}: {_game.PlayerOne.Name} had {round.PLayerOnesChoice}, {_game.PlayerTwo.Name} had {round.PlayerTwosChoice} -- {round.Winner.Name} wins.");
            }
        }
    }
}
