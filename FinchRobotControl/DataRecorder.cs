using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinchRobotControl
{
    class DataRecorder
    {
        internal static int GetNumberOfDataPoints()
        {
            int numberOfDataPoints = 0;
            Program.DisplayScreenHeader("Number of Data Points");

            numberOfDataPoints = Validation.ValidateIntResponse("How many number of Data Points?\n");

            Program.DisplayContinuePrompt();
            return numberOfDataPoints;

        }
        internal static double GetDataPointFrequency()
        {
            double dataPointFrequency;
            Program.DisplayScreenHeader("Data Point Frequency");

            dataPointFrequency = Validation.ValidateDoubleResponse("Frequency of Data Points?\n");

            Program.DisplayContinuePrompt();
            return dataPointFrequency;
        }

        internal static double[] GetTemperatureDataRecorderData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {

            double[] temperatures = new double[numberOfDataPoints];
            double tempCelsius = 0;
            Program.DisplayScreenHeader("Get Data");

            Console.WriteLine($"Number of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"Data Point Frequency: {dataPointFrequency}\n");
            Console.WriteLine("Starting temperature recording\n");

            int awaitTime = (int)(dataPointFrequency * 100);

            for (int i = 0; i < numberOfDataPoints; i++)
            {
                tempCelsius = finchRobot.getTemperature();
                temperatures[i] = ConvertCelsiusToFahrenheit(tempCelsius);
                Console.WriteLine($"Reading {i + 1}: {temperatures[i].ToString("n2")}");
                finchRobot.wait(awaitTime);
            }

            Program.DisplayContinuePrompt();

            return temperatures;
        }
        private static double ConvertCelsiusToFahrenheit(double tempCelsius)
        {
            return (tempCelsius * 1.8) + 32;
        }

        internal static void DisplayData(double[] temperatures)
        {
            Program.DisplayScreenHeader("Display Data");
            DisplayDataTable(temperatures);
            Program.DisplayContinuePrompt();

        }
        internal static void DisplayDataTable(double[] data)
        {
            Console.WriteLine(
                "Recording #".PadLeft(10) +
                "Temp".PadLeft(10));
            Console.WriteLine(
                "___________".PadLeft(10) +
                "___________".PadLeft(10));

            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(
                    (i + 1).ToString().PadLeft(10) +
                    data[i].ToString("n2").PadLeft(10));
            }
        }
        internal static void GetLightDataRecorderData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            int[] allSensorData = new int[numberOfDataPoints];
            int[] leftLightArray = new int[numberOfDataPoints];
            int leftLight = 0;

            int[] rightLightArray = new int[numberOfDataPoints];
            int rightLight = 0;

            Console.WriteLine($"Number of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"Data Point Frequency: {dataPointFrequency}\n");
            Console.WriteLine("Starting Light recording\n");

            int awaitTime = (int)(dataPointFrequency * 100);

            for (int x = 0; x < numberOfDataPoints; x++)
            {
                leftLight = finchRobot.getLeftLightSensor();
                leftLightArray[x] = leftLight;

                rightLight = finchRobot.getRightLightSensor();
                rightLightArray[x] = rightLight;

                finchRobot.wait(awaitTime);
                Console.WriteLine($"{leftLightArray[x]}, {rightLightArray[x]}");

            }

            DisplaySensorData(leftLightArray, rightLightArray);

            Program.DisplayContinuePrompt();
        }

        private static void DisplaySensorData(int[] leftLightArray, int[] rightLightArray)
        {
            Console.Clear();

            if(leftLightArray.Length == rightLightArray.Length)
            {
                Array.Sort(leftLightArray);
                Array.Sort(rightLightArray);
                Console.WriteLine($"Left Sensor Values: | Right Sensor Values:\n");

                for (int i = 0; i < leftLightArray.Length; i++)
                {
                    Console.WriteLine($" {leftLightArray[i]}                   {rightLightArray[i]}");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"The sum of the left light levels is {leftLightArray.Sum()}");
            Console.WriteLine($"The average light level from the left sensor is {leftLightArray.Average():n2}\n");

            Console.WriteLine($"The sum of the right light levels is {rightLightArray.Sum()}");
            Console.WriteLine($"The average light level from the right sensor is {rightLightArray.Average():n2}");

        }
    }
}
