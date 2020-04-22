using System;

//Ryan Shereda, JD Heckenliable, Michael Hall
namespace rpsProject
{
    class Program
    {
        //number tie count
        private static int tieCount = 0;
        //round counter
        private static int roundNumber = 1;

        static void Main(string[] args)
        {
            //create player 1 object
            Player player1;
            //create player 2 object
            Player player2;

            //set player 1 name (readLine)
            System.Console.WriteLine("Player 1, please input your name:");
            player1 = new Player(Console.ReadLine());
            //set player 2 name (readLine)
            System.Console.WriteLine("Player 2, please input your name:");
            player2 = new Player(Console.ReadLine());

            //repeat main gameplay method until one wins - Do while loop
            do
            {
                //run main gameplay method
                MainGameplay(player1, player2);
            } while ((player1.Score < 2) && (player2.Score < 2));    //ends when one player has 2 or more points


            //if player 1 score more than 2
            if (player1.Score >= 2) {
                //write to console "[player1] wins [player1.score]-[player2.score] with [tie count] ties"
                System.Console.WriteLine($"{player1.Name} wins {player1.Score}-{player2.Score} with {tieCount} tie(s).");
            } else if (player2.Score >= 2) {   //} else if player 2 score more than 2 {
                //write to console "[player2] wins [player2.score]-[player1.score] with [tie count] ties"
                System.Console.WriteLine($"{player2.Name} wins {player2.Score}-{player1.Score} with {tieCount} tie(s).");
            } else {
                System.Console.WriteLine($" Player 1 score: {player1.Score}");
                System.Console.WriteLine($" Player 2 score: {player2.Score}");
                System.Console.WriteLine("Critical Failure");
            }
        }

        //main gameplay method(round # parameter)
        private static void MainGameplay(Player player1, Player player2)
        {
            var rnd = new Random();
            //variable to hold winner
            Player winner = null;
            //variable to hold player 1 choice
            //player 1 gets random enum/number
            var player1Choice = (Choice)rnd.Next(0,3);
            //variable to hold player 2 choice
            //player 2 gets random enum/number
            var player2Choice = (Choice)rnd.Next(0, 3);

            //if statement checking player 1's choice
            if (player1Choice == Choice.rock) {
                //nested if statement checking player 2's choice
                if (player2Choice == Choice.paper) {
                    //player 2 wins
                    winner = player2;
                } else if (player2Choice == Choice.scissors) {
                    //player 1 wins
                    winner = player1;
                }
            } else if (player1Choice == Choice.paper) {
                //nested if statement checking player 2's choice
                if (player2Choice == Choice.scissors) {
                    //player 2 wins
                    winner = player2;
                } else if (player2Choice == Choice.rock) {
                    //player 1 wins
                    winner = player1;
                }
            } else if (player1Choice == Choice.scissors) {
                //nested if statement checking player 2's choice
                if (player2Choice == Choice.rock) {
                    //player 2 wins
                    winner = player2;
                } else if (player2Choice == Choice.paper) {
                    //player 1 wins
                    winner = player1;
                }
            }

            //check winner ([IF] there is a tie)
            if(player1Choice == player2Choice) {
                //write there was a tie
                System.Console.WriteLine($"Round {roundNumber}: {player1.Name} and {player2.Name} had {player1Choice} -- No winner.");
            
                //increment tie counter
                tieCount++;
            } else {        //[ELSE] a player won
                //Winner's score + 1
                 winner.IncreaseScore();

                //write results to console
                //round # - [player1] had [value], [player2] had [value] - [winner] won
                System.Console.WriteLine($"Round {roundNumber}: {player1.Name} had {player1Choice}, {player2.Name} had {player2Choice} -- {winner.Name} wins.");            
            }
            roundNumber++;
        }
    } 
}
