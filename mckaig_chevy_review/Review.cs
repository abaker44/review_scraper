using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace mckaig_chevy_review
{
    public class Review
    {
        public Review(string reviewer, string reviewTitle, string reviewBody, int rating)
        {
            Reviewer = reviewer;
            ReviewTitle = reviewTitle;
            ReviewBody = reviewBody;
            Rating = rating;
            CalculateReviewWeight();

        }

        public string Reviewer { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewBody { get; set; }
        public int Rating { get; set; }
        public int ReviewWeight { get; set; }


        //This Method calculates the "ReviewWeight" it 
        //first counts the occurences of each word in the review and title
        //second checks those words against a dictionary of positive and negative words defined in the WordLists Class
        //third it takes the product of the assigned weight of the positive or negative word with the number of 
        //occurences in the review
        //fourth it add that product to the current weight 
        //if the word is not found in the word list it adds 0 
        private void CalculateReviewWeight()
        {
            //Intial weight of the review is the rating it recieved
            int reviewWeight = Rating;
            //this will hold the word from the review as a key and the number of occurences as the value
            Dictionary<string, int> wordCount = new Dictionary<string, int> { };
            //this intiates a new wordcount object
            WordLists wordLists = new WordLists();

            //This creates an array for every word in the reivew, and make each lower case
            string review = ReviewBody.ToLower() + " " + ReviewTitle.ToLower();
            string ratingString = Regex.Replace(review, @"[\']", "");
            char[] delimiters = new[] { ',', ';', ' ', '.' };
            var wordArr = review.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < wordArr.Length; i++)
            {
                int currentCount;
                //this checks if the word in the review is in the word count dictionary 
                wordCount.TryGetValue(wordArr[i], out currentCount);
                //if it is found it increments the count by one if it is not found it adds it to the dictionary 
                wordCount[wordArr[i]] = currentCount + 1;
            }

            foreach (var word in wordCount)
            {
                int currentCount;
                //this checks if the word in the review matches a word in the positve dictionary 
                wordLists.PositiveWords.TryGetValue(word.Key, out currentCount);
                //if it is found it multiplies the occurences in the review by the weight of the word
                reviewWeight += currentCount * word.Value;

                wordLists.NegativeWords.TryGetValue(word.Key, out currentCount);
                reviewWeight += currentCount * word.Value;
            }

            ReviewWeight = reviewWeight;
        }
    }
}
