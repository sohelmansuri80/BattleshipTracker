using System;
using System.Threading;
using Battleship.Core;
using Battleship.Services;

namespace Battleship.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many games do you want to play?");
            int n = int.Parse(Console.ReadLine());
            BattleshipService service = new BattleshipService();
            for (int i = 0; i < n; i++)
            {
                service.StartGame();
                Console.WriteLine(service.DisplayBoards());

                while (!service.IsGameOver())
                {
                    // Uncomment following two-lines if you want to take charge
                    //Console.WriteLine("Press any key to play next turn.......");
                    //Console.Read();
                    service.PlayTurn();
                    Console.WriteLine(service.DisplayBoards());
                    
                    // Grab popcorn and enjoy the battle
                   //Thread.Sleep(500);
                }
                Console.WriteLine(service.DisplayBoards());
                Console.WriteLine(service.GetWinnerStatistics());
            }

            Console.Read();
        }
    }
}
