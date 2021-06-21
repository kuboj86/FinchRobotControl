using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinchRobotControl
{
    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        DONE
    }
    class Program
    {
        // **************************************************
        //
        // Title: Finch Control - Menu Starter
        // Description: Starter solution with the helper methods,
        //              opening and closing screens, and the menu
        // Application Type: Console
        // Author: Jason Kubo
        // Dated Created: 1/22/2020
        // Last Modified: 6/19/2021
        //
        // **************************************************
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            FinchAPI.Finch finchRobot = new FinchAPI.Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit\n");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        AlarmDisplayMenuScreen(finchRobot);
                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }



        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(FinchAPI.Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) ");
                Console.WriteLine("\tc) ");
                Console.WriteLine("\td) ");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case "b":

                        break;

                    case "c":

                        break;

                    case "d":

                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLightAndSound(FinchAPI.Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region DATA RECORDER
        private static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {
            bool quitDataRecorderMenu = false;
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatues = null;

            while (!quitDataRecorderMenu)
            {
                DisplayScreenHeader("Data Recorder Menu");

                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Temperature Data");
                Console.WriteLine("\td) Get Light Data");
                Console.WriteLine("\te) Show Data");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");

                string menuChoice = Console.ReadLine();
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorder.GetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorder.GetDataPointFrequency();
                        break;

                    case "c":
                        temperatues = DataRecorder.GetTemperatureDataRecorderData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        DataRecorder.GetLightDataRecorderData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "e":
                        DataRecorder.DisplayData(temperatues);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }

        #endregion
        #region AlarmSystem
        private static void AlarmDisplayMenuScreen(Finch finchRobot)
        {
            bool quitDataRecorderMenu = false;
            string monitorType;

            while (!quitDataRecorderMenu)
            {
                DisplayScreenHeader("Light Alarm Menu");

                Console.WriteLine("\ta) Monitor Light or Temperature");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "a":
                        monitorType = AlarmSystem.GetTypeToMonitor();
                        if (monitorType == "light")
                        {
                            SetLightSensorAlarm(finchRobot);
                        }
                        else if(monitorType == "temperature")
                        {
                            SetTemperatureSensorAlarm(finchRobot);
                        }
                        else
                        {
                            SetAlarmTempAndLight(finchRobot);
                        }
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }

        private static void SetAlarmTempAndLight(Finch finchRobot)
        {
            bool quitDataRecorderMenu = false;

            List<AlarmSystem> LightAndTemp = new List<AlarmSystem>();
            AlarmSystem alarmToMonitor = new AlarmSystem();
            //temp.monitorType = "temperature";
            
            //AlarmSystem light = new AlarmSystem();
            //light.monitorType = "light";


            
            while (!quitDataRecorderMenu)
            {
                for (int i = 0; i < 2; i++)
                {

                    if (i == 0)
                    {
                        alarmToMonitor = SetTemperatureSensorAlarm(finchRobot, "Temperature");
                    }
                    else
                    {
                        alarmToMonitor = SetLightSensorAlarm(finchRobot, "Light");
                    }
                    LightAndTemp.Add(alarmToMonitor);
                }
                quitDataRecorderMenu = true;
            }
            Console.Clear();
            string response = Validation.ValidateYesNo($"Set alarm? Y/N");
            if (response == "yes")
            {
                AlarmSystem.SetLightAndTempAlarm(finchRobot, LightAndTemp);
            }
            else
            {
                Console.WriteLine("OK, returning to the main menu");
            }
        }

        private static void SetTemperatureSensorAlarm(Finch finchRobot)
        {
            bool quitDataRecorderMenu = false;

            AlarmSystem alarm = new AlarmSystem();
            alarm.monitorType = "Temperature";
            while (!quitDataRecorderMenu)
            {
                DisplayScreenHeader("Temperature Alarm Menu");

                Console.WriteLine("\ta) Set Range Type");
                Console.WriteLine("\tb) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\tc) Set Time to Monitor");
                Console.WriteLine("\td) Set Alarm");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {

                    case "a":
                        alarm.rangeType = AlarmSystem.SetRangeType(alarm.monitorType);
                        break;

                    case "b":
                        alarm.minMaxThresholdValue = AlarmSystem.SetMinMaxThresholdValue(alarm, finchRobot);
                        break;

                    case "c":
                        alarm.timeToMonitor = AlarmSystem.SetTimeToMonitor(alarm.monitorType);
                        break;

                    case "d":
                        AlarmSystem.SetTemperatureAlarm(finchRobot, alarm);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }

        private static AlarmSystem SetTemperatureSensorAlarm(Finch finchRobot, string monitorType)
        {
            bool quitDataRecorderMenu = false;

            AlarmSystem alarm = new AlarmSystem();
            alarm.monitorType = monitorType;
            while (!quitDataRecorderMenu)
            {
                DisplayScreenHeader("Temperature Alarm Menu");

                Console.WriteLine("\ta) Set Range Type");
                Console.WriteLine("\tb) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\tc) Set Time to Monitor");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {

                    case "a":
                        alarm.rangeType = AlarmSystem.SetRangeType(alarm.monitorType);
                        break;

                    case "b":
                        alarm.minMaxThresholdValue = AlarmSystem.SetMinMaxThresholdValue(alarm, finchRobot);
                        break;

                    case "c":
                        alarm.timeToMonitor = AlarmSystem.SetTimeToMonitor(alarm.monitorType);
                        break;
                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
            return alarm;
        }

        private static void SetLightSensorAlarm(Finch finchRobot)
        {
            bool quitDataRecorderMenu = false;


            AlarmSystem alarm = new AlarmSystem();
            alarm.monitorType = "Light";

            while (!quitDataRecorderMenu)
            {
                DisplayScreenHeader("Light Alarm Menu");

                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {

                    case "a":
                        alarm.sensorsToMonitor = AlarmSystem.SetSensorsToMonitor();
                        break;

                    case "b":
                        alarm.rangeType = AlarmSystem.SetRangeType(alarm.monitorType);
                        break;

                    case "c":
                        alarm.minMaxThresholdValue = AlarmSystem.SetMinMaxThresholdValue(alarm, finchRobot);
                        break;

                    case "d":
                        alarm.timeToMonitor = AlarmSystem.SetTimeToMonitor(alarm.monitorType);
                        break;

                    case "e":
                        AlarmSystem.SetLightAlarm(finchRobot, alarm);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }
        private static AlarmSystem SetLightSensorAlarm(Finch finchRobot, string monitorType)
        {
            bool quitDataRecorderMenu = false;


            AlarmSystem alarm = new AlarmSystem();
            alarm.monitorType = monitorType;

            while (!quitDataRecorderMenu)
            {
                DisplayScreenHeader("Light Alarm Menu");

                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {

                    case "a":
                        alarm.sensorsToMonitor = AlarmSystem.SetSensorsToMonitor();
                        break;

                    case "b":
                        alarm.rangeType = AlarmSystem.SetRangeType(alarm.monitorType);
                        break;

                    case "c":
                        alarm.minMaxThresholdValue = AlarmSystem.SetMinMaxThresholdValue(alarm, finchRobot);
                        break;

                    case "d":
                        alarm.timeToMonitor = AlarmSystem.SetTimeToMonitor(alarm.monitorType);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
            return alarm;
        }


        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(FinchAPI.Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        public static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        public static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
