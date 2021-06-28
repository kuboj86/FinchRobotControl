using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinchRobotControl
{
    class FinchConnectivity
    {
        public static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            Program.DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            Program.DisplayContinuePrompt();


            //This will not work currently because this app was built in .NET Core
            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnected.");

            Program.DisplayMenuPrompt("Main Menu");
        }
        public static void DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected = false;

            Program.DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            Program.DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            if (robotConnected == true)
            {
                Console.WriteLine("\tConnection established with Finch");
                FinchConnectedStartup(finchRobot);
            }

            Program.DisplayMenuPrompt("Main Menu");
        }

        public static Finch FinchConnectedStartup(Finch finchRobot)
        {
            finchRobot.setMotors(-100, 100);
            finchRobot.wait(300);
            finchRobot.setMotors(100, -100);
            finchRobot.wait(300);
            finchRobot.setMotors(0, 0);
            for (int i = 0; i < 3; i++)
            {

                finchRobot.setLED(0, 255, 0);
                finchRobot.wait(500);
                finchRobot.setLED(0, 0, 0);

                finchRobot.noteOn(i);
                finchRobot.wait(500);
                finchRobot.noteOff();

            }

            finchRobot.setLED(0, 255, 0);
            return finchRobot;
        }
    }
}
