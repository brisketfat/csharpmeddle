using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
        // this is the main method for our program
        static void Main(string[] args)
		{
            string choice = MainMenu();

            switch (choice)
            {
                case "1":
                    OutputRandomNumber();
                    break;

                case "2":
                    OutputNCharsNTimes();
                    break;
            }

            if (Again())
            {
                string[] empty = { "" };
                Main(empty);
            }
		}

        // asks the user if they want to do it all over again, returns bool
        static bool Again()
        {
            Console.WriteLine("Would you like to do it again?");
            string choice = Console.ReadLine();

            string[] nopes = { "N", "n", "No", "NO", "no" };
            int pos = Array.IndexOf(nopes, choice);

            if (pos > -1)
            {
                return false;
            }
            return true;
        }

        // outputs the main menu and returns the users choice
        static string MainMenu()
        {
            Console.WriteLine("Please make a selection:");

            string[] selections = {
                "Output a Random Number",
                "Output n Characters n Times"
            };

            int i = 1;
            foreach (string selection in selections)
            {
                Console.WriteLine(i + ": " + selection);
                i++;
            }
            
            string choice = Console.ReadLine();

            return choice;
        }

        // outputs a random number
        static void OutputRandomNumber()
        {
            Random rand = new Random();

            Console.WriteLine(rand.Next());

            Console.ReadLine();
        }

        // outputs N characters, N times
        static void OutputNCharsNTimes()
        {
            Console.WriteLine("Enter a number:");
            string n = Console.ReadLine();

            int num = Int32.Parse(n);


            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();

            for (int i = 0; i < num; i++) {
                string result = new string(Enumerable.Repeat(chars, num)
                    .Select(s => s[rand.Next(s.Length)]).ToArray());

                Console.WriteLine(result);
            }

            Console.ReadLine();
            
        }
	}
}
