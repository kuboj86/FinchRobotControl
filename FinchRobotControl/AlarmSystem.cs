using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinchRobotControl
{
    public class AlarmSystem
    {
        public string monitorType { get; set; }
        public string sensorsToMonitor { get; set; }
        public string rangeType { get; set; }
        public int minMaxThresholdValue { get; set; }
        public int timeToMonitor { get; set; }
        public bool isParametersMet { get; set; }

        internal static string GetTypeToMonitor()
        {
            Program.DisplayScreenHeader("Select Alarm Type");

            string choice;
            choice = Validation.ValidateMonitorType("Would you like to monitor Light, Temperature, or Both?\n");

            return choice;
        }
        internal static string SetSensorsToMonitor()
        {
            string sensorsToMonitor;

            Program.DisplayScreenHeader("Sensors to Monitor");
            sensorsToMonitor = Validation.ValidateSensorOption("Sensors to monitor? Left, Right, Both\n");

            return sensorsToMonitor;
        }

        internal static string SetRangeType(string monitorType)
        {
            string rangeType;

            Program.DisplayScreenHeader($"{monitorType} Range Type");
            rangeType = Validation.ValidateRangeTypeOption("Range Type: Minimum, Maximum\n");

            return rangeType;
        }

        internal static int SetMinMaxThresholdValue(AlarmSystem alarm, Finch finchRobot)
        {
            int thresholdValue;

            Program.DisplayScreenHeader($"{alarm.monitorType} Minimum - Maximum Threshold Value");

            if(alarm.monitorType == "Light")
            {
                Console.WriteLine($"Left light sensor value is {finchRobot.getLeftLightSensor()}\n" +
                                $"Right light sensor value is {finchRobot.getRightLightSensor()}");
            }
            else
            {
                double temp = GetTemperatureCurrentValue(finchRobot);
                Console.WriteLine($"Current tempurature is {temp}\n");
            }
            thresholdValue = Validation.ValidateIntResponse($"Enter the {alarm.rangeType} range value");

            Console.WriteLine($"{alarm.rangeType} of {thresholdValue} has been selected.");

            Program.DisplayMenuPrompt($"{alarm.monitorType} Alarm");

            return thresholdValue;
        }
        internal static int SetTimeToMonitor(string monitorType)
        {
            int timeToMonitor;

            Program.DisplayScreenHeader($"{monitorType} Time to Monitor");

            timeToMonitor = Validation.ValidateIntResponse($"Time to Monitor");

            Console.WriteLine($"");

            Program.DisplayMenuPrompt($"{monitorType} Alarm");

            return timeToMonitor;
        }

        internal static void SetLightAlarm(Finch finchRobot, AlarmSystem alarm)
        {
            int secondsElapsed = 0;
            bool isThresholdExceeded = false;

            Program.DisplayScreenHeader("Set Light Alarm");

            Console.WriteLine($"Sensors to monitor: {alarm.sensorsToMonitor}");
            Console.WriteLine($"Range Type: {alarm.rangeType}");
            Console.WriteLine($"Minimum/Maximum threshold value: {alarm.minMaxThresholdValue}");
            Console.WriteLine($"Time to monitor: {alarm.timeToMonitor}\n");

            Program.DisplayContinuePrompt();
            Console.Clear();
             
            while((secondsElapsed < alarm.timeToMonitor) && !isThresholdExceeded)
            {

                int currentLightSensorValue = GetLightSensorsCurrentValue(alarm.sensorsToMonitor, finchRobot);
                isThresholdExceeded = GetLightThresholdExceeded(currentLightSensorValue, alarm.minMaxThresholdValue, alarm.rangeType);

                SetLightValuesOnScreen(currentLightSensorValue.ToString(),secondsElapsed, alarm.monitorType);
                finchRobot.wait(1000);
                secondsElapsed++;

                if (isThresholdExceeded && alarm.rangeType == "minimum")
                {
                    SetOffAlarm(finchRobot,$"WARNING: The Light level has fallen below the {alarm.rangeType} threshold of {alarm.minMaxThresholdValue}. Please be advised");
                }
                else if(isThresholdExceeded && alarm.rangeType == "maximum")
                {
                    SetOffAlarm(finchRobot,$"WARNING: The Light level has exceeded  the {alarm.rangeType} threshold of {alarm.minMaxThresholdValue}. Please be advised");
                }
            }

            Program.DisplayMenuPrompt("Light Alarm");
        }
        internal static void SetLightAndTempAlarm(Finch finchRobot, List<AlarmSystem> alarms)
        {
            int secondsElapsed = 0;
            bool isThresholdExceeded = false;

            Program.DisplayScreenHeader("Set Light and Temperature Alarm");

            //find the alarm type with the longest time to determine how long the while loop runs for
            int maxTimeToMonitor = alarms.Max(x => x.timeToMonitor);

            foreach (var item in alarms)
            {
                Console.WriteLine($"{item.monitorType} Alarm Settings"); // add new line
                if(item.monitorType == "Light")
                {
                    Console.WriteLine($"Sensors to monitor: {item.sensorsToMonitor}");

                }
                Console.WriteLine($"Range Type: {item.rangeType}");
                Console.WriteLine($"Minimum/Maximum threshold value: {item.minMaxThresholdValue}");
                Console.WriteLine($"Time to monitor: {maxTimeToMonitor} seconds\n");
            }


            Program.DisplayContinuePrompt();
            Console.Clear();

            while ((secondsElapsed < maxTimeToMonitor) && !isThresholdExceeded)
            {
                foreach (var alarm in alarms)
                {
                    if(alarm.monitorType == "Light")
                    {
                        int currentLightSensorValue = GetLightSensorsCurrentValue(alarm.sensorsToMonitor, finchRobot);
                        isThresholdExceeded = GetLightThresholdExceeded(currentLightSensorValue, alarm.minMaxThresholdValue, alarm.rangeType);

                        SetLightValuesOnScreen(currentLightSensorValue.ToString(), secondsElapsed, alarm.monitorType);
                        finchRobot.wait(1000);
                        secondsElapsed++;

                        if (isThresholdExceeded && alarm.rangeType == "minimum")
                        {
                            SetOffAlarm(finchRobot, $"WARNING: The Light level has fallen below the {alarm.rangeType} threshold of {alarm.minMaxThresholdValue}. Please be advised");
                        }
                        else if (isThresholdExceeded && alarm.rangeType == "maximum")
                        {
                            SetOffAlarm(finchRobot, $"WARNING: The Light level has exceeded  the {alarm.rangeType} threshold of {alarm.minMaxThresholdValue}. Please be advised");
                        }
                    }
                    else
                    {
                        double currentLightSensorValue = GetTemperatureCurrentValue(finchRobot);
                        isThresholdExceeded = GetTemperatureThresholdExceeded(currentLightSensorValue, alarm.minMaxThresholdValue, alarm.rangeType);

                        SetTempValuesOnScreen(currentLightSensorValue.ToString(), secondsElapsed, alarm.monitorType);
                        finchRobot.wait(1000);
                        secondsElapsed++;

                        if (isThresholdExceeded && alarm.rangeType == "minimum")
                        {
                            SetOffAlarm(finchRobot, $"WARNING: The Temperature level has fallen below the {alarm.rangeType} threshold of {alarm.minMaxThresholdValue}. Please be advised");
                        }
                        else if (isThresholdExceeded && alarm.rangeType == "maximum")
                        {
                            SetOffAlarm(finchRobot, $"WARNING: The Temperature level has exceeded  the {alarm.rangeType} threshold of {alarm.minMaxThresholdValue}. Please be advised");
                        }
                    }
                }
            }

            Program.DisplayMenuPrompt("Light Alarm");
        }


        private static void SetOffAlarm(Finch finchRobot, string message)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(message);
                finchRobot.noteOn(2093);
                finchRobot.wait(800);
                finchRobot.noteOff();
            }
        }

        private static int GetLightSensorsCurrentValue(string sensorsToMonitor, Finch finchRobot)
        {
            int currentLightSensorValue = 0;
            switch (sensorsToMonitor)
            {
                case "left":
                    currentLightSensorValue = finchRobot.getLeftLightSensor();
                    break;
                case "right":
                    currentLightSensorValue = finchRobot.getRightLightSensor();
                    break;
                case "both":
                    currentLightSensorValue = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
                    break;

            }
            return currentLightSensorValue;
        }

        private static bool GetLightThresholdExceeded(int currentLightSensorValue, int minMaxThresholdValue, string rangeType)
        {
            bool isThresholdExceeded = false;

            switch (rangeType)
            {
                case "minimum":
                    if (currentLightSensorValue < minMaxThresholdValue)
                    {
                        isThresholdExceeded = true;
                    }
                    break;
                case "maximum":
                    if (currentLightSensorValue >= minMaxThresholdValue)
                    {
                        isThresholdExceeded = true;
                    }
                    break;
            }

            return isThresholdExceeded;
        }
        internal static void SetTemperatureAlarm(Finch finchRobot, AlarmSystem alarm)
        {
            int secondsElapsed = 0;
            bool isThresholdExceeded = false;

            Program.DisplayScreenHeader("Set Temperature Alarm");

            Console.WriteLine($"Range Type: {alarm.rangeType}");
            Console.WriteLine($"Minimum/Maximum threshold value: {alarm.minMaxThresholdValue}");
            Console.WriteLine($"Time to monitor: {alarm.timeToMonitor}\n");

            Program.DisplayContinuePrompt();
            Console.Clear();

            while ((secondsElapsed < alarm.timeToMonitor) && !isThresholdExceeded)
            {

                double currentLightSensorValue = GetTemperatureCurrentValue(finchRobot);
                isThresholdExceeded = GetTemperatureThresholdExceeded(currentLightSensorValue, alarm.minMaxThresholdValue, alarm.rangeType);

                SetTempValuesOnScreen(currentLightSensorValue.ToString(), secondsElapsed, alarm.monitorType);
                finchRobot.wait(1000);
                secondsElapsed++;

                if (isThresholdExceeded && alarm.rangeType == "minimum")
                {
                    SetOffAlarm(finchRobot, $"WARNING: The Temperature level has fallen below the {alarm.rangeType} threshold of {alarm.minMaxThresholdValue}. Please be advised");
                }
                else if (isThresholdExceeded && alarm.rangeType == "maximum")
                {
                    SetOffAlarm(finchRobot, $"WARNING: The Temperature level has exceeded  the {alarm.rangeType} threshold of {alarm.minMaxThresholdValue}. Please be advised");
                }
            }
        }
        private static double GetTemperatureCurrentValue(Finch finchRobot)
        {
            double currentTemperatureValue = 0;

            currentTemperatureValue = finchRobot.getTemperature();
            double tempFahrenheit = (currentTemperatureValue * 1.8) + 32;

            return tempFahrenheit;
        }
        private static bool GetTemperatureThresholdExceeded(double currentTempValue, int minMaxThresholdValue, string rangeType)
        {
            bool isThresholdExceeded = false;

            switch (rangeType)
            {
                case "minimum":
                    if (currentTempValue < minMaxThresholdValue)
                    {
                        isThresholdExceeded = true;
                    }
                    break;
                case "maximum":
                    if (currentTempValue >= minMaxThresholdValue)
                    {
                        isThresholdExceeded = true;
                    }
                    break;
            }

            return isThresholdExceeded;
        }
        private static void SetLightValuesOnScreen(string currentLightSensorValue, int secondsElapsed, string monitorType)
        {
            Console.SetCursorPosition(5, 10);
            Console.WriteLine($"Current {monitorType} Level: {currentLightSensorValue}");

            Console.SetCursorPosition(5, 12);
            Console.WriteLine($"Time Elapsed: {secondsElapsed}\n");
        }
        private static void SetTempValuesOnScreen(string currentLightSensorValue, int secondsElapsed, string monitorType)
        {
            Console.SetCursorPosition(30, 10);
            Console.WriteLine($"Current {monitorType} Level: {currentLightSensorValue}");

            Console.SetCursorPosition(30, 12);
            Console.WriteLine($"Time Elapsed: {secondsElapsed}\n");
        }
    }
}
