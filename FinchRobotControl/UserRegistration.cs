using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinchRobotControl
{
    class UserRegistration
    {
        internal static void LoginRegistrationMenuScreen()
        {
            Program.DisplayScreenHeader("Login Menu");

            string choice;
            choice = Validation.ValidateYesNo("Are you already Registered? Y/N\n");
            switch (choice)
            {
                case "yes":
                    UserLogin();
                break;

                case "no":
                    RegisterNewUser();
                    UserLogin();

                    break;

                default:
                    break;

            }
        }

        private static void RegisterNewUser()
        {
            string userName;
            string password;

            Program.DisplayScreenHeader("Register New User");

            Console.WriteLine("Enter in Username");
            userName = Console.ReadLine();
            Console.WriteLine("Enter in Password");
            password = Console.ReadLine();

            InsertNewUser(userName, password);

            Console.WriteLine($"Username : {userName}");
            Console.WriteLine($"Password : {password}");

            Program.DisplayContinuePrompt();
        }

        private static void InsertNewUser(string userName, string password)
        {
            string directory = $"Data/Users.txt";
            string entry = $"{userName},{password}";

            File.WriteAllText(directory, entry);
        }

        private static void UserLogin()
        {
            string userName;
            string password;
            bool isValid = false;

            while (!isValid)
            {
                Program.DisplayScreenHeader("Login Screen");

                Console.WriteLine("Enter in Username");
                userName = Console.ReadLine();
                Console.WriteLine("Enter in Password");
                password = Console.ReadLine();

                isValid = GetValidUserInfo(userName, password);

                if (isValid)
                {
                    Console.WriteLine($"You are now logged in {userName}!");
                }
                else
                {
                    Console.WriteLine("The Username or Password is incorrect.");
                    Console.WriteLine("Please try again.");
                }
                Program.DisplayContinuePrompt();
            }
        }

        private static bool GetValidUserInfo(string userName, string password)
        {
            List<(string userName, string password)> userCredentials = new List<(string userName, string password)>();
            bool isValid;

            userCredentials = GetUserInfoData();

            isValid = (userCredentials.userName == userName && userCredentials.password == password);

            return isValid;
        }

        private static List<(string userName, string password)> GetUserInfoData()
        {
            string file = $"Data/Users.txt";

            string credentials;
            string[] credentialArray;
            (string userName, string password) loginInfo;

            List<(string userName, string password)> registeredUsers = new List<(string userName, string password)>();

            credentials = File.ReadAllText(file);

            credentialArray = credentials.Split(',');
            loginInfo.userName = credentialArray[0];
            loginInfo.password = credentialArray[1];

            return loginInfo;
        }
    }
}
