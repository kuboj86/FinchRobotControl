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
            (string userName, string password) userCredentials;
            string choice;
            choice = Validation.ValidateYesNo("Are you already Registered? Y/N\n");
            switch (choice)
            {
                case "yes":
                    UserLogin();
                break;

                case "no":
                    userCredentials = RegisterNewUser();
                    InsertNewUser(userCredentials);
                    UserLogin();

                    break;

                default:
                    break;

            }
        }

        private static (string userName, string password) RegisterNewUser()
        {
            (string userName, string password) userCredentials;

            Program.DisplayScreenHeader("Register New User");

            Console.WriteLine("Enter in Username");
            userCredentials.userName = Console.ReadLine();
            Console.WriteLine("Enter in Password");
            userCredentials.password = Console.ReadLine();

            Console.WriteLine($"Username : {userCredentials.userName}");
            Console.WriteLine($"Password : {userCredentials.password}");

            Program.DisplayContinuePrompt();

            return userCredentials;
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
                    CreateNewUsersScreen();
                }
                else
                {
                    Console.WriteLine("The Username or Password is incorrect.");
                    Console.WriteLine("Please try again.");
                }
                Program.DisplayContinuePrompt();
            }
        }

        private static void CreateNewUsersScreen()
        {
            Program.DisplayScreenHeader("Add New Users Screen");
            try
            {
                List<(string userName, string password)> userList = new List<(string userName, string password)>();
                List<string> userCredentials = new List<string>();

                (string userName, string password) userCredential;

                bool isContinue = true;
                string choice;
                while (isContinue)
                {
                    choice = Validation.ValidateYesNo("Would you like to add more user? Y/N\n");

                    switch (choice)
                    {
                        case "yes":
                            userCredential = RegisterNewUser();
                            userList.Add(userCredential);
                            break;
                        case "no":
                            isContinue = false;
                            break;
                    }
                }
                foreach (var userCred in userList)
                {
                    string entry = $"{userCred.userName},{userCred.password}";
                    userCredentials.Add(entry);
                }
                InsertNewUserList(userCredentials);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private static void InsertNewUser((string userName, string password) userCredentials)
        {
            string directory = $"Data/Users.txt";
            string entry = $"{userCredentials.userName},{userCredentials.password}";

            File.WriteAllText(directory, entry);
        }
        private static void InsertNewUserList(List<string> userList)
        {
            string directory = $"Data/Users.txt";

            File.WriteAllLines(directory, userList);
        }

        private static bool GetValidUserInfo(string userName, string password)
        {
            List<(string userName, string password)> userCredentials = new List<(string userName, string password)>();
            bool isValid = false;

            userCredentials = GetUserInfoData();

            foreach ((string userName, string password) userLoginInfo in userCredentials)
            {
                if((userLoginInfo.userName == userName) && (userLoginInfo.password == password))
                {
                    isValid = true;
                    break;
                }
            }
            

            return isValid;
        }

        private static List<(string userName, string password)> GetUserInfoData()
        {
            string file = $"Data/Users.txt";

            string credentials;
            string[] credentialArray;
            (string userName, string password) loginInfo;

            List<(string userName, string password)> registeredUsers = new List<(string userName, string password)>();

            credentialArray = File.ReadAllLines(file);

            foreach (var item in credentialArray)
            {
                credentialArray = item.Split(',');
                loginInfo.userName = credentialArray[0];
                loginInfo.password = credentialArray[1];

                registeredUsers.Add(loginInfo);

            }

            return registeredUsers;
        }
    }
}
