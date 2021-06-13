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

        internal static double[] GetDataRecorderData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
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
        internal static void DisplayDataTable(double[] temperatures)
        {
            Console.WriteLine(
                "Recording #".PadLeft(10) +
                "Temp".PadLeft(10));
            Console.WriteLine(
                "___________".PadLeft(10) +
                "___________".PadLeft(10));

            for (int i = 0; i < temperatures.Length; i++)
            {
                Console.WriteLine(
                    (i + 1).ToString().PadLeft(10) +
                    temperatures[i].ToString("n2").PadLeft(10));
            }
        }
    }
}
