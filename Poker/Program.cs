using System;
using System.IO;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("--------------------POKER GAME----------------------");
                Console.WriteLine("Play Game, Please press any key");
                Console.WriteLine("Exit Game, Please press Escape key");
                Console.WriteLine("----------------------------------------------------");
                var userKey = Console.ReadKey(true);

                while (userKey.Key != ConsoleKey.Escape)
                {
                    string textFile;//= @"D:\POC\pokergame.txt";
                    Console.Write(@"Enter File name (Ex. C:\MyFile.txt):");
                    textFile = Console.ReadLine();

                    if (!File.Exists(textFile.Trim()))
                    {
                        Console.WriteLine("The file not exists.");
                    }
                    else
                    {
                        PokerManager manager = new PokerManager();
                        manager.LoadData(textFile);
                    }

                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("Continue Game, Please press any key");
                    Console.WriteLine("Exit Game, Please press Escape key");
                    Console.WriteLine("----------------------------------------------------");
                    userKey = Console.ReadKey(true);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
           
        }
    }
}
