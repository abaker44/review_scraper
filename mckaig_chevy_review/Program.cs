using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace mckaig_chevy_review
{
    class Program
    {
        const String reviewBaseURL = "http://www.dealerrater.com/dealer/McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685/";
        const int pagesToGet = 5;
        static void Main(string[] args)
        {
            ScrapingService scrapingService = new ScrapingService(reviewBaseURL);
            List<Review> reviews = scrapingService.getReviews(pagesToGet);
           reviews =  reviews.OrderByDescending(x => x.ReviewWeight).ToList();
            Review[] topThree = reviews.Take(3).ToArray();
            Console.WriteLine("Three Most Positive Reviews");

            for (int i = 0; i < topThree.Length; i++)
            {
                Console.WriteLine(topThree[i].Reviewer);
                Console.WriteLine(topThree[i].ReviewTitle);
                Console.WriteLine(topThree[i].ReviewBody);
                Console.WriteLine();


            }
            Console.ReadKey();
        }
       
    }
}
