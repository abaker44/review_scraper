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
    public class ReviewTests
    {
        Review review = new Review("reviewer", "title", "cool fantastic cool cool", 4);
        [TestMethod()]
        public void CalculateReviewWeightTest()
        {
            review.CalculateReviewWeight();
            int assignedWeight = review.ReviewWeight;
            Assert.AreEqual(14, assignedWeight);
        }

        [TestMethod()]
        public void getArrayOfWords()
        {
            string stringToSplit = review.ReviewTitle + " " + review.ReviewBody;
            string[] stringArr = review.getArrayOfWords(stringToSplit);
            string[] expectedArr = new string[] { "title", "cool", "fantastic", "cool", "cool", };
            bool areEqual = stringArr.SequenceEqual(expectedArr);
            Assert.IsTrue(areEqual);

        }

        [TestMethod()]
        public void getWordCountOfArray()
        {
            string stringToSplit = review.ReviewTitle + " " + review.ReviewBody;
            string[] stringArr = review.getArrayOfWords(stringToSplit);
            var wordCount = review.getWordCountOfArray(stringArr);
            Dictionary<string, int> expected = new Dictionary<string, int> {{"title", 1},
                {"cool", 3}, {"fantastic", 1 } };
            bool areEqual = wordCount.SequenceEqual(expected);
            Assert.IsTrue(areEqual);
        }

        [TestMethod()]
        public void getReviewWeight()
        {
            string stringToSplit = review.ReviewTitle + " " + review.ReviewBody;
            string[] stringArr = review.getArrayOfWords(stringToSplit);
            var wordCount = review.getWordCountOfArray(stringArr);
           int expectedWeight = review.getReviewWeight(wordCount, review.Rating);

            Assert.AreEqual(14, expectedWeight);
        }
    }
}