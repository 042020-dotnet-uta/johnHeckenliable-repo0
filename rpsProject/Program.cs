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
            new Game().StartGame();
        }
    } 
}
