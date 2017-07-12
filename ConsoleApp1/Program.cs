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
            // capture the users choice            
            string choice = MainMenu();

            // do the selected choice
            switch (choice)
            {
                case "1":
                    OutputRandomNumber();
                    break;

                case "2":
                    OutputNCharsNTimes();
                    break;
            }

            // repeat or not
            if (Again())
            {
                string[] empty = { "" };
                Main(empty);
            }
		}

        // asks the user if they want to do it all over again, returns bool
        static bool Again()
        {
            // output question, read input
            Console.WriteLine("Would you like to do it again?");
            string choice = Console.ReadLine();

            // all the possible No values
            string[] nopes = { "N", "n", "No", "NO", "no" };
            int pos = Array.IndexOf(nopes, choice);

            // retrn bool
            if (pos > -1)
            {
                return false;
            }
            return true;
        }

        // outputs the main menu and returns the users choice
        static string MainMenu()
        {
            // output question
            Console.WriteLine("Please make a selection:");

            // array of choices
            string[] selections = {
                "Output a Random Number",
                "Output n Characters n Times"
            };

            // output choices
            int i = 1;
            foreach (string selection in selections)
            {
                Console.WriteLine(i + ": " + selection);
                i++;
            }
            
            // capture user choice
            string choice = Console.ReadLine();

            // return choice
            return choice;
        }

        // outputs a random number
        static void OutputRandomNumber()
        {
            // instantiate random class
            Random rand = new Random();

            // output random number
            Console.WriteLine(rand.Next());

            // wait for user (just a pause)
            Console.ReadLine();
        }

        // outputs N characters, N times
        static void OutputNCharsNTimes()
        {
            // ask user to enter number, capture
            Console.WriteLine("Enter a number:");
            string n = Console.ReadLine();

            // convert to int
            int num = Int32.Parse(n);

            // array of characters to pick from
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            // instantiate random class
            Random rand = new Random();

            // loop n times and output string
            for (int i = 0; i < num; i++) {
                // i found this here: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
                string result = new string(Enumerable.Repeat(chars, num)
                    .Select(s => s[rand.Next(s.Length)]).ToArray());

                Console.WriteLine(result);
            }

            // wait for user (just a pause)
            Console.ReadLine();
        }
	}
}
