using System;
//using System.Collections.Generic;
//using System.Linq;
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
                Main({ "" });
            }

		}

        static bool Again()
        {
            Console.WriteLine("Would you like to do it again?");
            string choice = Console.ReadLine();

            if (choice == "N") return false;
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

            // get a number from the user, output N random characters, N times

            Console.ReadLine();
        }
	}
}
