using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsHeadlines
{
    public class Helpers
    {
        public static string? GetUserInput(string prompt)
        {
            System.Console.Write(prompt + " ");
            ResetConsoleColor();
            string? inputLine = System.Console.ReadLine();

            return string.IsNullOrEmpty(inputLine) ? null : inputLine;
        }

        public static void SearchTimeout()
        {
            System.Console.WriteLine();
            SetConsoleColor("blue");
            System.Console.Write("Searching");
            System.Threading.Thread.Sleep(500); System.Console.Write(".");
            System.Threading.Thread.Sleep(500); System.Console.Write(".");
            System.Threading.Thread.Sleep(500); System.Console.Write(".");
            ResetConsoleColor();

            System.Threading.Thread.Sleep(2000);
            System.Console.Clear();
        }

        public static async Task EndOfProgram()
        {
            string? qInput = GetUserInput("➡️ Press Q to quit or R to restart:");

            while (true)
            {
                if (string.IsNullOrEmpty(qInput) || (qInput.ToLower() != "r" && qInput.ToLower() != "q"))
                {
                    SetConsoleColor("red");
                    qInput = GetUserInput("❌ Please make a valid selection:");
                    ResetConsoleColor();
                }
                else break;
            }

            if (qInput.ToLower().Equals("q"))
            {
                Quit();
            }
            else if (qInput.ToLower().Equals("r"))
            {
                System.Console.Clear();
                await Program.Main();
            }
        }

        public static void Quit()
        {
            System.Environment.Exit(0); // Exiting program
        }

        public static void PrintAscii(string input)
        {
            switch (input)
            {
                // Print "Welcome to Console.News"
                case "main":
                    System.Console.WriteLine("__        __   _                            _                \r\n\\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___          \r\n \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\         \r\n  \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |        \r\n  _\\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/         \r\n / ___|___  _ __  ___  ___ | | ___  | \\ | | _____      _____ \r\n| |   / _ \\| '_ \\/ __|/ _ \\| |/ _ \\ |  \\| |/ _ \\ \\ /\\ / / __|\r\n| |__| (_) | | | \\__ \\ (_) | |  __/_| |\\  |  __/\\ V  V /\\__ \\\r\n \\____\\___/|_| |_|___/\\___/|_|\\___(_)_| \\_|\\___| \\_/\\_/ |___/");
                    break;
                // Print "Top Stories:"
                case "top":
                    System.Console.WriteLine(" _____             ____  _             _               \r\n|_   _|__  _ __   / ___|| |_ ___  _ __(_) ___  ___   _ \r\n  | |/ _ \\| '_ \\  \\___ \\| __/ _ \\| '__| |/ _ \\/ __| (_)\r\n  | | (_) | |_) |  ___) | || (_) | |  | |  __/\\__ \\  _ \r\n  |_|\\___/| .__/  |____/ \\__\\___/|_|  |_|\\___||___/ (_)\r\n          |_|                                          ");
                    break;
                // Print "Results:"
                case "search":
                    System.Console.WriteLine(" ____                 _ _           \r\n|  _ \\ ___  ___ _   _| | |_ ___   _ \r\n| |_) / _ \\/ __| | | | | __/ __| (_)\r\n|  _ <  __/\\__ \\ |_| | | |_\\__ \\  _ \r\n|_| \\_\\___||___/\\__,_|_|\\__|___/ (_)");
                    break;
                default:
                    System.Console.WriteLine("Title not found.");
                    break;
            }
            System.Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }

        public static void SetConsoleColor(string color)
        {
            switch (color)
            {
                case "red":
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "yellow":
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "blue":
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "cyan":
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "magenta":
                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "gray":
                    System.Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "darkgray":
                    System.Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                default:
                    break;
            }
        }

        public static void ResetConsoleColor()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
