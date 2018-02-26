using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuParsing
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ValidateArguments(args))
                Environment.Exit(-1);

            var menu = MenuParser.ParseFromFile(args[0]);

            menu.SetActive(args[1]);

            Console.Write(menu.ToString());
            Console.ReadLine();
        }

        private static bool ValidateArguments(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("USAGE: menuparsing.exe <path to menu file> <path to match>");
                return false;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File {args[0]} does not exist. Please specify a valid path to an XML file.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(args[1]))
            {
                Console.WriteLine($"Path {args[1]} in null or empty. Please specify valid menu item path.");
                return false;
            }

            return true;
        }
    }
}
