using System;

//Ryan Shereda, JD Heckenliable, Michael Hall
namespace rpsProject
{
    //create enumerated list for each possible choice
    //{rock, paper, scissors}
    public enum Choice
    {
        rock,
        paper,
        scissors
    }

    class Program
    {
        //number tie count
        int tieCount = 0;
        //round counter
        int roundNumber = 1;

        static void Main(string[] args)
        {
            
            //create player 1 object
            Player player1 = new Player();
            //create player 2 object
            Player player2 = new Player();

            MainGameplay(player1, player2);     
            //set player 1 name (readLine)
            //set player 2 name (readLine)

            //repeat main gameplay method until one wins
            //Do while loop {
            //run main gameplay method
            //} ends when one player has more than 2 points

            //if player 1 score more than 2
            //write to console "[player1] wins [player1.score]-[player2.score] with [tie count] ties"
            //} else if player 2 score more than 2 {
            //write to console "[player2] wins [player2.score]-[player1.score] with [tie count] ties"
            //}
        }

        //main gameplay method(round # parameter){
        public static void MainGameplay(Player player1, Player player2)
        {
            Random rnd = new Random();
            //variable to hold winner
            Player winner;
            //variable to hold player 1 choice
            Choice player1Choice;
            //variable to hold player 2 choice
            Choice player2Choice;

            //player 1 gets random enum/number
            player1Choice = (Choice)rnd.Next(0,3);
            //player 2 gets random enum/number
            player2Choice = (Choice)rnd.Next(0,3);

            //if statement checking player 1's choice
            if (player1Choice == Choice.rock) {
                //nested if statement checking player 2's choice
                if (player2Choice == Choice.paper) {
                    //player 2 wins
                    winner = player2;
                    //player2.score + 1;
                } else if (player2Choice == Choice.scissors) {
                    //player 1 wins
                    winner = player1;
                    //player1.score + 1;
                }
            } else if (player1Choice == Choice.paper) {
                //nested if statement checking player 2's choice
                if (player2Choice == Choice.scissors) {
                    //player 2 wins
                    winner = player2;
                    //player2.score + 1;
                } else if (player2Choice == Choice.rock) {
                    //player 1 wins
                    winner = player1;
                    //player1.score + 1;
                }
            } else if (player1Choice == Choice.scissors) {
                //nested if statement checking player 2's choice
                if (player2Choice == Choice.rock) {
                    //player 2 wins
                    winner = player2;
                    //player2.score + 1;
                } else if (player2Choice == Choice.paper) {
                    //player 1 wins
                    winner = player1;
                    //player1.score + 1;
                }
            }

            System.Console.WriteLine($"player1Choice: {player1Choice}");
            System.Console.WriteLine($"player2Choice: {player2Choice}");
            //check winner ([IF] there is a tie)
            // if(winner == null) {
            //     //write there was a tie
            //     System.Console.WriteLine($"Round #: Player1 and Player2 had {player1Choice} -- No winner.");
            
            //     //increment tie counter
            //     tieCount++;
            // } else {        //[ELSE] a player won
            //     //add score out here instead?
                
            //     //write results to console
            //     //round # - [player1] had [value], [player2] had [value] - [winner] won
            //     System.Console.WriteLine($"Round #: Player1 had {player1Choice}, Player2 had {player2Choice} -- {winner} wins.");            
            // }

        }
    }

    //small struct to hold player names/scores {
    public struct Player
    {
        //player name variable holder
        public String playerName;
        //player score variable holder
        public int playerScore;

        //because this is public, don't need getters/setters

        //initialize player variables(name) method
        //public void SetPlayerName(String setPlayerName)
        //{
        //    playerName = setPlayerName;
        //}
    }
    
}
