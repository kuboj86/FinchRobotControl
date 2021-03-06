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
        NOTEON,
        NOTEOFF,
        GETTEMP,
        PLAYMUSIC,
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
        // Last Modified: 6/27/2021
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

            Finch finchRobot = new Finch();

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
                Console.WriteLine("\tf) Login / Registration");
                Console.WriteLine("\tg) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit\n");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        FinchConnectivity.DisplayConnectFinchRobot(finchRobot);
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
                        UserProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        UserRegistration.LoginRegistrationMenuScreen();
                        break;

                    case "g":
                        FinchConnectivity.DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        FinchConnectivity.DisplayDisconnectFinchRobot(finchRobot);
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

        private static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
        {
            string menuChoice;
            bool quitMenu = false;

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();
            while (!quitMenu)
            {
                DisplayScreenHeader("User Programming Menu");

                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Quit\n");
                Console.Write("\t\tEnter Choice:");

                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgramming.GetCommandParameters();
                        break;
                    case "b":
                        UserProgramming.GetFinchCommands(commands);
                        break;
                    case "c":
                        UserProgramming.GetSelectedFinchCommands(commands);
                        break;
                    case "d":
                        UserProgramming.ExecuteFinchCommands(commands, finchRobot, (commandParameters.motorSpeed, commandParameters.ledBrightness, commandParameters.waitSeconds));
                        break;
                    case "q":
                        quitMenu = true;
                        break;
                }
            }
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
                DisplayScreenHeader("Alarm Menu");

                Console.WriteLine("\ta) Alarm Type\n");
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
            
            while (!quitDataRecorderMenu)
            {
                for (int i = 0; i < 2; i++)
                {

                    if (i == 0)
                    {
                        alarmToMonitor = SetTemperatureSensorAlarm(finchRobot, "Temperature");
                        if(alarmToMonitor.quitToMenu == true)
                        {
                            break;
                        }
                    }
                    else
                    {
                        alarmToMonitor = SetLightSensorAlarm(finchRobot, "Light");
                        if (alarmToMonitor.quitToMenu == true)
                        {
                            break;
                        }
                    }
                    LightAndTemp.Add(alarmToMonitor);
                }
                quitDataRecorderMenu = true;
            }
            if(LightAndTemp.Count == 2)
            {
                Console.Clear();
                string response = Validation.ValidateYesNo($"\tReady to Set alarms for Light and Temperature? Y/N");
                if (response == "yes")
                {
                    AlarmSystem.SetLightAndTempAlarm(finchRobot, LightAndTemp);
                }
                else
                {
                    Console.WriteLine("OK, returning to the main menu");
                }
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
                Console.WriteLine("\td) Set Light Alarm Parameters");
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
                        quitDataRecorderMenu = true;
                        break;
                    case "q":
                        quitDataRecorderMenu = true;
                        alarm.quitToMenu = true;
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
                        quitDataRecorderMenu = true;
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        alarm.quitToMenu = true;
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
