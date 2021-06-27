using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinchRobotControl
{
    class UserProgramming
    {
        internal static (int motorSpeed, int ledBrightness, double waitSeconds) GetCommandParameters()
        {
            Program.DisplayScreenHeader("Command Paramters");

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;

            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            commandParameters.motorSpeed = Validation.ValidateIntResponse("Enter in the Motor Speed of 1 - 255", 1, 255);
            commandParameters.ledBrightness = Validation.ValidateIntResponse("Enter in the LED Brightness of 1 - 255", 1, 255);
            commandParameters.waitSeconds = Validation.ValidateDoubleResponse("Enter in the", 1, 10);

            Console.WriteLine();
            Console.WriteLine($"Motor Speed: {commandParameters.motorSpeed}");
            Console.WriteLine($"LED Brightness: {commandParameters.ledBrightness}");
            Console.WriteLine($"Wait Command Duration: {commandParameters.waitSeconds}");

            Program.DisplayMenuPrompt("User Programming");

            return commandParameters;
        }

        internal static void GetFinchCommands(List<Command> commandList)
        {
            Command command = new Command();

            Program.DisplayScreenHeader("Finch Robot Commands");

            int commandCount = 1;
            foreach (var item in Enum.GetNames(typeof(Command)))
            {
                Console.WriteLine($"{item}");
                commandCount++;
            }

            while(command != Command.DONE)
            {
                Console.WriteLine("Enter a command:");

                if(Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commandList.Add(command);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter in a valid command from the list");
                }
            }

            Console.Clear();
            Console.WriteLine($"The following commands were selected\n");
            foreach (var item in commandList)
            {
                Console.WriteLine($"{item}");
            }

            Program.DisplayMenuPrompt($"User Programming");
        }

        internal static void GetSelectedFinchCommands(List<Command> commands)
        {
            Program.DisplayScreenHeader("Finch Robot Commands");

            Console.WriteLine($"The following commands were selected\n");

            foreach (var command in commands)
            {
                Console.WriteLine($"{command}");
            }

            Program.DisplayMenuPrompt($"User Programming");
        }

        internal static void ExecuteFinchCommands(List<Command> commands, Finch finchRobot, (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = (int)(commandParameters.waitSeconds * 1000);

            string commandName = "";
            const int TURNIN_MOTOR_SPEED = 100;

            Program.DisplayScreenHeader("Execute Finch COmmands");

            Console.WriteLine("The finch robot is ready to execute the list of commands");
            Program.DisplayContinuePrompt();

            foreach (var command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandName = Command.MOVEFORWARD.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        commandName = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        commandName = Command.STOPMOTORS.ToString();
                        break;

                    case Command.WAIT:
                        finchRobot.wait(waitMilliSeconds);
                        commandName = Command.WAIT.ToString();
                        break;

                    case Command.TURNLEFT:
                        finchRobot.setMotors(-TURNIN_MOTOR_SPEED, TURNIN_MOTOR_SPEED);
                        commandName = Command.TURNLEFT.ToString();
                        break;

                    case Command.TURNRIGHT:
                        finchRobot.setMotors(TURNIN_MOTOR_SPEED, -TURNIN_MOTOR_SPEED);
                        commandName = Command.TURNRIGHT.ToString();
                        break;

                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandName = Command.LEDON.ToString();
                        break;

                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        commandName = Command.LEDOFF.ToString();
                        break;

                    case Command.DONE:
                        commandName = Command.DONE.ToString();
                        break;

                    default:
                        break;

                        
                }
                Console.WriteLine($"{commandName}");
            }

            Program.DisplayMenuPrompt("User Programming");
        }
    }
}
