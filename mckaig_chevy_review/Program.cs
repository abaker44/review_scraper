using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace mckaig_chevy_review
{

    class Program
    {
        //This is the url that will take you to the review page, we just add "page1", "page2"...  to the end to get the additional pages
        const string reviewBaseURL = "http://www.dealerrater.com/dealer/McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685/";
        //This defines the numbers of pages you want scraped currently there are up to 120 
        const int pagesToGet = 5;
        //This defines how many reviews to print out
        const int reviewsToPrint = 3;

        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            ScrapingService scrapingService = new ScrapingService(reviewBaseURL);
            //This gets all the reviews from the specified pages and puts them into a list of Review objects 
            List<Review> reviews = scrapingService.getReviews(pagesToGet);
            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            //these are statistics out put 
            Console.WriteLine("Time Elapsed: " + elapsedMs / 1000 + " seconds");
            Console.WriteLine("Approx " + Math.Round(reviews.Count / (elapsedMs / 1000)) + " Reviews per second");
            Console.WriteLine();
            //this prints to the console the number of top reviews
            printReviews(reviews, reviewsToPrint);
            //this prints to a file all of the reviews in order
            Console.WriteLine("Print to file? (y/n) ");

            if(Console.ReadKey().Key == ConsoleKey.Y)
            {
                printToFile(reviews);
                Console.WriteLine();
                Console.WriteLine("File has been printed");
                Console.ReadKey();
            }
        }



        static void printReviews(List<Review> reviews, int numberToPrint)
        {
            //order by best reviews first, if you want the worst first change "OrderByDescending" to "OrderBy"
            reviews = reviews.OrderByDescending(x => x.ReviewWeight).ToList();

            Review[] reviewsToPrint;
            //This is to hande if the desired results to print it larger than the results, it will just print the max
            if (numberToPrint <= reviews.Count)
            {
                reviewsToPrint = reviews.Take(numberToPrint).ToArray();
            }
            else
            {
                reviewsToPrint = reviews.Take(reviews.Count).ToArray();
            }

            Console.WriteLine(reviewsToPrint.Length + " OUT OF " + reviews.Count + " Most Positive Reviews");
            //this is to extend the buffer so when you print reviews to console it doesn't get cut off
            Console.BufferWidth = Int16.MaxValue - 1;

            for (int i = 0; i < reviewsToPrint.Length; i++)
            {
                Console.WriteLine("Reviewer:  " + reviewsToPrint[i].Reviewer);
                Console.WriteLine("Rating: " + reviewsToPrint[i].Rating);
                Console.WriteLine("Weight Assigned: " + reviewsToPrint[i].ReviewWeight);
                Console.WriteLine("Title: " + reviewsToPrint[i].ReviewTitle);
                Console.WriteLine("Review: " + reviewsToPrint[i].ReviewBody);
                Console.WriteLine();
            }
        }

        static void printToFile(List<Review> reviews)
        {
            //order by best reviews first, if you want the worst first change "OrderByDescending" to "OrderBy"
            reviews = reviews.OrderByDescending(x => x.ReviewWeight).ToList();

            Review[] reviewsToPrint;
            //takes all of the reviews to the array to print 
            reviewsToPrint = reviews.Take(reviews.Count).ToArray();


            using (StreamWriter writetext = new StreamWriter("reviews.txt"))
            {
                //prints all the info to a file named reviews.txt
                for (int i = 0; i < reviewsToPrint.Length; i++)
                {
                    writetext.WriteLine("Reviewer:  " + reviewsToPrint[i].Reviewer);
                    writetext.WriteLine("Rating: " + reviewsToPrint[i].Rating);
                    writetext.WriteLine("Weight Assigned: " + reviewsToPrint[i].ReviewWeight);
                    writetext.WriteLine("Title: " + reviewsToPrint[i].ReviewTitle);
                    writetext.WriteLine("Review: " + reviewsToPrint[i].ReviewBody);
                    writetext.WriteLine();
                }
            }
        }




    }

}
