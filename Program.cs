namespace NewsHeadlines
{
    internal class Program
    {
        public static async Task Main()
        {
            Helpers.ResetConsoleColor();
            Helpers.PrintAscii("main");

            // Prompt for user choice
            System.Console.WriteLine("Menu:\n[1] Discover top headlines.\n[2] Search for your own headlines.\n[3] Quit");
            string? choice = Helpers.GetUserInput("\n➡️ Make a selection:");
            
            while (true)
            {
                if (string.IsNullOrEmpty(choice) || (choice != "1" && choice != "2" && choice!= "3")) 
                {
                    Helpers.SetConsoleColor("red");
                    choice = Helpers.GetUserInput("❌ Please make a valid selection:");
                    Helpers.ResetConsoleColor();
                }
                else break;
            }
            
            // Run API methods based on user choice
            switch (choice)
            {
                case "1":
                {
                    ApiHelpers.PrintPopularArticles();
                    break;
                }
                case "2":
                {
                    string? userQuery = Helpers.GetUserInput("\n🔎 Search for a topic:");

                    while (true)
                    {
                        if (string.IsNullOrEmpty(userQuery))
                        {
                            Helpers.SetConsoleColor("red");
                            userQuery = Helpers.GetUserInput("❌ Invalid input. What topic do you want to search for?:");
                            Helpers.ResetConsoleColor();
                        }
                        else break;
                    }

                    await ApiHelpers.PrintSearchedArticlesAsync(userQuery);
                    break;
                }
                case "3":
                {
                    Helpers.Quit();
                    break;
                }
                default: break;
            }

            await Helpers.EndOfProgram(); // Prompt user to restart or quit
        }        
    }
}