using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			int number;
			number = 1;

			while (number != 0)
			{
				Console.WriteLine("Enter a number between 1 and 10 (0 to exit):");

				number = int.Parse(Console.ReadLine());


				if (number > 0 && number <= 10)
				{
					Console.WriteLine("Good");
				}
				else
				{
					Console.WriteLine("Bad");
				}

			}


		}
	}
}
