using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;

namespace NewsHeadlines
{
    public class ApiHelpers
    {
        // Method to fetch and print articles based on a search query
        public static async Task PrintSearchedArticlesAsync(string query)
        {
            // Init with your API key
            var newsApiClient = new NewsApiClient("667cf68eaa6e48b0b06f3bf0a9590003");
            // var newsApiClient = new NewsApiClient("fbbc8a18e6934ad49468e2a21663801c");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = query,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                //  From = new DateTime(2018, 1, 25)
            });

            // Check if the API request was successful
            if (articlesResponse.Status == Statuses.Ok)
            {
                Helpers.SearchTimeout();
                Helpers.PrintAscii("search");

                // Total results found count
                if (articlesResponse.TotalResults != 0)
                {
                    System.Console.Write("🔎 \"" + query + "\": ");
                    Helpers.SetConsoleColor("green");
                    System.Console.Write(articlesResponse.TotalResults + " results found. ");
                    if (articlesResponse.TotalResults > 20)
                    {
                        System.Console.Write("Showing top 20 results.");
                    }
                    Helpers.ResetConsoleColor();
                    System.Console.WriteLine();
                    System.Console.WriteLine("\n——————————————————————————————————————————————————————————————————————\n");
                }

                // Print each article from API response
                int max = 20;
                for (int i = 0; i < max && i < articlesResponse.TotalResults; i++)
                {
                    Article thisArticle = articlesResponse.Articles[i];

                    string shortDesc = thisArticle.Description.Split('.')[0];

                    if (thisArticle.Title == "[Removed]")
                    {
                        max++;
                    }
                    else 
                    {
                        await Task.Delay(200);

                        // Print Title
                        System.Console.WriteLine(thisArticle.Title);

                        // Print Description
                        if (!string.IsNullOrEmpty(shortDesc))
                        {
                            Helpers.SetConsoleColor("cyan");
                            System.Console.WriteLine(shortDesc + ".");
                            Helpers.ResetConsoleColor();
                        }

                        // Print Author
                        if (!string.IsNullOrEmpty(thisArticle.Author))
                        {
                            System.Console.WriteLine();
                            Helpers.SetConsoleColor("yellow");
                            System.Console.WriteLine(thisArticle.Author);
                            Helpers.ResetConsoleColor();
                        }

                        // Print Published DateTime
                        if (!string.IsNullOrEmpty(thisArticle.PublishedAt.ToString()))
                        {
                            Helpers.SetConsoleColor("yellow");
                            System.Console.WriteLine(thisArticle.PublishedAt);
                            Helpers.ResetConsoleColor();
                        }

                        // Print URL
                        if (!string.IsNullOrEmpty(thisArticle.Url))
                        {
                            Helpers.SetConsoleColor("darkgray");
                            System.Console.WriteLine("\nVisit: " + thisArticle.Url);
                            Helpers.ResetConsoleColor();
                        }

                        System.Console.WriteLine("\n——————————————————————————————————————————————————————————————————————\n");
                    }
                }

                if (articlesResponse.TotalResults == 0)
                {
                    Helpers.SetConsoleColor("red");
                    System.Console.WriteLine("🔎 No results found.");
                    Helpers.ResetConsoleColor();
                    System.Console.WriteLine();
                }
            }
        }


        // Method to fetch and print popular articles
        public static void PrintPopularArticles()
        {
            // Init with your API key
            var newsApiClient = new NewsApiClient("667cf68eaa6e48b0b06f3bf0a9590003");
            //alt key: 

            var articlesResponse = newsApiClient.GetTopHeadlines(new TopHeadlinesRequest
            {
                Q = "",
                Language = Languages.EN,
                //  From = new DateTime(2018, 1, 25)
            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                Helpers.SearchTimeout();
                Helpers.PrintAscii("top");

                // Total results found count
                System.Console.Write("🔎 Popular: ");
                Helpers.SetConsoleColor("green");
                System.Console.Write(articlesResponse.TotalResults + " results found. Showing top 20 results.\n");
                Helpers.ResetConsoleColor();
                System.Console.WriteLine("\n——————————————————————————————————————————————————————————————————————\n");

                // Print each article from API response
                foreach (var article in articlesResponse.Articles)
                {
                    System.Threading.Thread.Sleep(200);

                    // Print Title
                    System.Console.WriteLine(article.Title);

                    // Print Author
                    Helpers.SetConsoleColor("yellow");
                    System.Console.WriteLine(article.Author);
                    Helpers.ResetConsoleColor();

                    // Print Published DateTime
                    Helpers.SetConsoleColor("yellow");
                    System.Console.WriteLine(article.PublishedAt);
                    Helpers.ResetConsoleColor();

                    // Print URL
                    Helpers.SetConsoleColor("darkgray");
                    System.Console.WriteLine("\nVisit: " + article.Url);
                    Helpers.ResetConsoleColor();

                    System.Console.WriteLine("\n——————————————————————————————————————————————————————————————————————\n");
                }
            }
        }
    }
}