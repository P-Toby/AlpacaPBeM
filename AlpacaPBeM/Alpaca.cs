using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlpacaPBeM.Properties;

namespace AlpacaPBeM
{
    class Alpaca
    {
        
        //[STAThread]
        static void Main(string[] args)
        {
            TurnReceiver TurnReceiverManager = new TurnReceiver();
            TurnSender TurnSenderManager = new TurnSender();

            bool running = true;
            while(running)
            {
                try
                {
                    //Shitty way to test stuff until I get a proper UI implemented
                    Console.Clear();
                    Console.WriteLine("Alpaca PBeM v0.01\n");
                    Console.WriteLine("Type g to get turn\nType s to send turn\nq to quit");
                    string input = Console.ReadLine();

                    if (input.Contains("g"))
                    {
                        Console.WriteLine("Enter name of game to fetch from email:");
                        string name = Console.ReadLine();
                        TurnReceiverManager.GetTurn(name);
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();

                    }
                    else if (input.Contains("s"))
                    {
                        Console.WriteLine("Enter name of game to send to server:");
                        string name = Console.ReadLine();
                        TurnSenderManager.SendTurn(name);
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();

                    }
                    else if (input.Contains("q"))
                    {
                        running = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            


            Console.WriteLine("\nQuitting.");
        }
    }
}