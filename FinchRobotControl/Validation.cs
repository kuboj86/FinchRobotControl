using System;
using System.Collections.Generic;
using System.Text;

namespace FinchRobotControl
{
    public class Validation
    {
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

        public static string ValidateYesNo(string message)
        {
            bool isValid = true;
            string answer = "";
            while (isValid)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine().ToLower();
                if (input == "y")
                {
                    isValid = false;
                    answer = "yes";
                }
                else if (input == "n")
                {
                    isValid = false;
                }
                else
                {
                    isValid = true;
                    ValidateYesNo("Im sorry I didn't quite understand your response.\n");
                }
            }
            return answer;
        }
    }
}
