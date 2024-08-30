using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;
using Figgle;
using Colorful;
using System.Drawing;
using Console = Colorful.Console;

namespace NewsHeadlines
{
    internal class Program
    {
        static void Main()
        {
            ResetConsoleColor();
            PrintAscii("main");

            // Prompt for user choice
            System.Console.WriteLine("Menu:\n[1] Find top headlines.\n[2] Search for your own headlines.\n[3] Quit");
            string? choice = GetUserInput("\n➡️ Make a selection:");
            
            while (true)
            {
                if (string.IsNullOrEmpty(choice) || (choice != "1" && choice != "2" && choice!= "3")) 
                {
                    SetConsoleColor("red");
                    choice = GetUserInput("❌ Please make a valid selection:");
                    ResetConsoleColor();
                }
                else break;
            }
            
            // Run API methods based on user choice
            switch (choice)
            {
                case "1":
                {
                    PrintPopularArticles();
                    break;
                }
                case "2":
                {
                    string? userQuery = GetUserInput("\n🔎 Search for a topic:");

                    while (true)
                    {
                        if (string.IsNullOrEmpty(userQuery))
                        {
                            SetConsoleColor("red");
                            userQuery = GetUserInput("❌ Invalid input. What topic do you want to search for?:");
                            ResetConsoleColor();
                        }
                        else break;
                    }

                    System.Console.WriteLine();
                    PrintSearchedArticles(userQuery);
                    break;
                }
                case "3":
                {
                    Quit();
                    break;
                }
                default: break;
            }

            EndOfProgram(); // Prompt user to restart or quit
        }





        // API METHODS
        // --------------------------------

        static void PrintSearchedArticles(string query)
        {
            // Init with your API key
            var newsApiClient = new NewsApiClient("667cf68eaa6e48b0b06f3bf0a9590003");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = query,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                //  From = new DateTime(2018, 1, 25)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                SearchTimeout();
                PrintAscii("search");

                // Total results found
                //Console.WriteLine(articlesResponse.TotalResults);

                // Here's the first 20
                foreach (var article in articlesResponse.Articles)
                {
                    string shortDesc = article.Description.Split('.')[0];
                    //int punctuation  = shortDesc.IndexOf('.');
                    //shortDesc.Split('.')[0] = punctuation;
                    if (article.Title != "[Removed]" && article.Title.Contains(query))
                    {
                        System.Threading.Thread.Sleep(200);

                        // Print Title
                        System.Console.WriteLine(article.Title);

                        // Print Description
                        SetConsoleColor("cyan");
                        System.Console.WriteLine(shortDesc + ".");
                        ResetConsoleColor();

                        // Print Author
                        System.Console.WriteLine();
                        SetConsoleColor("yellow");
                        System.Console.WriteLine(article.Author);
                        ResetConsoleColor();

                        // Print Published DateTime
                        SetConsoleColor("yellow");
                        System.Console.WriteLine(article.PublishedAt);
                        ResetConsoleColor();

                        // Print URL
                        SetConsoleColor("darkgray");
                        System.Console.WriteLine("\nVisit: " + article.Url);
                        //Console.WriteLine(TrimURL(article.Url));
                        ResetConsoleColor();

                        System.Console.WriteLine("\n——————————————————————————————————————————————————————————————————————\n");
                    }
                }

                if (articlesResponse.TotalResults == 0)
                {
                    SetConsoleColor("red");
                    System.Console.WriteLine("🔎 No results found.");
                    ResetConsoleColor();
                    System.Console.WriteLine();
                }
            }
        }

        static void PrintPopularArticles()
        {
            // Init with your API key
            var newsApiClient = new NewsApiClient("667cf68eaa6e48b0b06f3bf0a9590003");
            var articlesResponse = newsApiClient.GetTopHeadlines(new TopHeadlinesRequest
            {
                Q = "",
                Language = Languages.EN,
                //  From = new DateTime(2018, 1, 25)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                SearchTimeout();
                PrintAscii("top");

                foreach (var article in articlesResponse.Articles)
                {
                    System.Threading.Thread.Sleep(200);

                    // Print Title
                    System.Console.WriteLine(article.Title);

                    // Print Author
                    SetConsoleColor("yellow");
                    System.Console.WriteLine(article.Author);
                    ResetConsoleColor();

                    // Print Published DateTime
                    SetConsoleColor("yellow");
                    System.Console.WriteLine(article.PublishedAt);
                    ResetConsoleColor();

                    // Print URL
                    SetConsoleColor("darkgray");
                    System.Console.WriteLine("\nVisit: " + article.Url);
                    ResetConsoleColor();

                    System.Console.WriteLine("\n——————————————————————————————————————————————————————————————————————\n");
                }
            }
            
        }





        // HELPERS
        // --------------------------------

        static string? GetUserInput(string prompt)
        {
            System.Console.Write(prompt + " ");
            ResetConsoleColor();
            string? inputLine = System.Console.ReadLine();

            return string.IsNullOrEmpty(inputLine) ? null : inputLine;
        }


        static void SearchTimeout()
        {
            System.Console.WriteLine();
            SetConsoleColor("blue");
            System.Console.Write("🔎 Searching");
            System.Threading.Thread.Sleep(500); System.Console.Write(".");
            System.Threading.Thread.Sleep(500); System.Console.Write(".");
            System.Threading.Thread.Sleep(500); System.Console.Write(".");
            ResetConsoleColor();

            System.Threading.Thread.Sleep(2000);
            System.Console.Clear();
        }

        public static void EndOfProgram()
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
                Main();
            }
        }

        public static void Quit()
        {
            System.Environment.Exit(0); // Exiting program
        }

        static void PrintAscii(string input)
        {
            switch (input)
            {
                // Print "Welcome to Console.News"
                case "main":
                  string mainArt =  "__        __   _                            _                \r\n\\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___          \r\n \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\         \r\n  \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |        \r\n  _\\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/         \r\n / ___|___  _ __  ___  ___ | | ___  | \\ | | _____      _____ \r\n| |   / _ \\| '_ \\/ __|/ _ \\| |/ _ \\ |  \\| |/ _ \\ \\ /\\ / / __|\r\n| |__| (_) | | | \\__ \\ (_) | |  __/_| |\\  |  __/\\ V  V /\\__ \\\r\n \\____\\___/|_| |_|___/\\___/|_|\\___(_)_| \\_|\\___| \\_/\\_/ |___/";
                     System.Console.WriteLine("__        __   _                            _                \r\n\\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___          \r\n \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\         \r\n  \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |        \r\n  _\\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/         \r\n / ___|___  _ __  ___  ___ | | ___  | \\ | | _____      _____ \r\n| |   / _ \\| '_ \\/ __|/ _ \\| |/ _ \\ |  \\| |/ _ \\ \\ /\\ / / __|\r\n| |__| (_) | | | \\__ \\ (_) | |  __/_| |\\  |  __/\\ V  V /\\__ \\\r\n \\____\\___/|_| |_|___/\\___/|_|\\___(_)_| \\_|\\___| \\_/\\_/ |___/");
                   // Colorful.Console.WriteLineFormatted(mainArt, Color.Purple);
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

        static void SetConsoleColor(string color)
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

        static void ResetConsoleColor()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
        }
    }
}