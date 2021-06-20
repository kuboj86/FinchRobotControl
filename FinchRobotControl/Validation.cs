using System;
using System.Collections.Generic;
using System.Text;

namespace FinchRobotControl
{
    public class Validation
    {
        public static string ValidateMonitorType(string message)
        {
            bool isValid = false;
            string choice = "";

            while (!isValid)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine().ToLower();
                if (input == "light" || input == "temperature" || input == "both")
                {
                    isValid = true;
                    choice = input;
                }
                else
                {
                    isValid = false;
                    Console.Clear();
                    Console.WriteLine("Im sorry I didn't quite understand your response.\n");
                }
            }

            return choice;
        }
        public static int ValidateIntResponse(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            int selection;
            if (int.TryParse(input, out selection))
            {
                return selection;
            }
            else
            {
                return ValidateIntResponse("I'm sorry I didn't quite get that answer.\n");
            }
        }
        public static double ValidateDoubleResponse(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            double selection;
            if (double.TryParse(input, out selection))
            {
                return selection;
            }
            else
            {
                return ValidateDoubleResponse("I'm sorry I didn't quite get that answer.\n");
            }
        }
        public static string ValidateSensorOption(string message)
        {
            bool isValid = false;
            string choice = "";
            
            while (!isValid)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine().ToLower();
                if (input == "left" || input == "right" || input == "both")
                {
                    isValid = true;
                    choice = input;
                }
                else
                {
                    isValid = false;
                    Console.Clear();
                    Console.WriteLine("Im sorry I didn't quite understand your response.\n");
                }
            }

            return choice;
        }
        public static string ValidateRangeTypeOption(string message)
        {
            bool isValid = false;
            string choice = "";

            while (!isValid)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine().ToLower();
                if (input == "minimum" || input == "maximum")
                {
                    isValid = true;
                    choice = input;
                }
                else
                {
                    isValid = false;
                    Console.Clear();
                    Console.WriteLine("Im sorry I didn't quite understand your response.\n");
                }
            }
            return choice;
        }
    }
}
