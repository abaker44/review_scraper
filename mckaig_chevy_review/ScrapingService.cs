using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mckaig_chevy_review
{

    public class ScrapingService
    {
        public ScrapingService(string reviewPageUrl)
        {
            ReviewPageUrl = reviewPageUrl;
        }

        private string ReviewPageUrl { get; set; }

        //The getReviews method is used to scrape the website
        //and put the information into a Review Object
        public List<Review> getReviews(int numberOfPages)
        {
            //this is what is returned a list of review objects 
            List<Review> reviews = new List<Review> { };
            //using multithreading to increase parsing time 
            Parallel.For(0, numberOfPages, i => {
               
                //this is how to review page is retrived 
                //IT IS DEPENDENT THAT THE URL SUPPLIED CAN HAVE "PAGEX" APPENED TO IT!!
                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(ReviewPageUrl + "page" + i);
                //using xpath we retrieve the parent div of each review
                var values = doc.DocumentNode.SelectNodes("//div[contains(@class, 'review-entry')]").ToList();
              

                    Parallel.ForEach(values, v =>
                    {
                    //for each review div we have to access other elements so we add an xpath the the parent div's xpath to get element
                    string reviewerName = doc.DocumentNode.SelectSingleNode(v.XPath + "//span[contains(@class, 'italic')]").InnerText;
                    string reviewTitle = doc.DocumentNode.SelectSingleNode(v.XPath + "//h3[contains(@class, 'no-format')]").InnerText;
                    string reviewBody = doc.DocumentNode.SelectSingleNode(v.XPath + "//p[contains(@class, 'review-content')]").InnerText;
                    //this is a hack, the parse cannot read apostrophe's so it inputs "&#39;" in their place this is just replacing that 
                    reviewBody = reviewBody.Replace("&#39;", "\'");
                    //the rating is found by the class of the div, the class is "review-50 (5 stars), review-40 (4 stars) ..."
                    string ratingClasses = doc.DocumentNode.SelectSingleNode(v.XPath + "//div[contains(@class, 'rating-static')]").GetAttributeValue("class", null);
                    //in ratingClasses we found all the classes of the rating div, now we narrow it down to just numbers 
                    string ratingString = Regex.Replace(ratingClasses, @"[^0-9]", "");
                        int rating = 0;
                    //we have to try to parse the rating, I did this because if the class for the rating 
                    //changes at all it would mess everything up 
                    try
                        {
                            rating = Int32.Parse(ratingString);
                            rating = rating / 10;

                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    //this is to ensure there is at least a review found if not we don't need to add it to the list
                    if (reviewBody != null)
                        {
                            Review newReview = new Review(reviewerName, reviewTitle, reviewBody, rating);
                            reviews.Add(newReview);
                        }

                    });
                });
            return reviews;
        }
      

    }
}
