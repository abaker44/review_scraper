using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace mckaig_chevy_review
{

    public class ScrapingService
    {
        public ScrapingService(string reviewPageUrl)
        {
            ReviewPageUrl = reviewPageUrl;
        }
        private string ReviewPageUrl { get; set; }

       public List<Review> getReviews(int numberOfPages)
        {
            List<Review> reviews = new List<Review> { };

            for (int i = 1; i < numberOfPages + 1; i++)
            {
                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(ReviewPageUrl + "page"  + i);
                var values = doc.DocumentNode.SelectNodes("//div[contains(@class, 'review-entry')]").ToList();

                foreach (var review in values)
                {
                    string reviewerName = doc.DocumentNode.SelectSingleNode(review.XPath + "//span[contains(@class, 'italic')]").InnerText;
                    string reviewTitle = doc.DocumentNode.SelectSingleNode(review.XPath + "//h3[contains(@class, 'no-format')]").InnerText;
                    string reviewBody = doc.DocumentNode.SelectSingleNode(review.XPath + "//p[contains(@class, 'review-content')]").InnerText;
                    string ratingClasses = doc.DocumentNode.SelectSingleNode(review.XPath + "//div[contains(@class, 'rating-static')]").GetAttributeValue("class", null);
                    string ratingString = Regex.Replace(ratingClasses, @"[^0-9]", "");
                    int rating = 0;
                    try
                    {
                        rating = Int32.Parse(ratingString);
                        rating = rating / 10;

                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    if (reviewBody != null)
                    {
                        Review newReview = new Review(reviewerName, reviewTitle, reviewBody, rating);
                        reviews.Add(newReview);
                    }

                }
            }
            return reviews;
        }


    }
}
