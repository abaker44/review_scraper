using Microsoft.VisualStudio.TestTools.UnitTesting;
using mckaig_chevy_review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mckaig_chevy_review.Tests
{
    [TestClass()]
    public class ScrapingServiceTests
    {
        ScrapingService scrapingSevice = new ScrapingService("http://www.dealerrater.com/dealer/McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685/");
        [TestMethod()]
        public void getReviewsTest()
        {
            List<Review> reviews = scrapingSevice.getReviews(1);
            Assert.AreEqual(10, reviews.Count);
        }
        [TestMethod()]
        public void getReviewsTest2()
        {
            List<Review> reviews = scrapingSevice.getReviews(2);
            Assert.AreEqual(20, reviews.Count);
        }
    }
}