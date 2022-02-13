using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            //minimal size of the console window
            if (Console.WindowHeight < 21 || Console.WindowWidth < 77)
            {
                Console.WriteLine("Insufficient window size, minimal size is 77x21");
                System.Environment.Exit(1);
            }

            //trying to catch any exceptions that could occur
            try
            {
                GameRunner game = new GameRunner();
                game.PlayGame();

            }
            catch (Exception e)
            {

                Console.WriteLine($"Exception raised: {e.GetType().ToString()} - {e.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                System.Environment.Exit(1);
            }


            System.Environment.Exit(0);
        }
    }
}
