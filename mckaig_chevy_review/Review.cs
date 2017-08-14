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


        
        public void CalculateReviewWeight()
        {
            //This gets array of each word in reviewBody
            var wordArr = getArrayOfWords(ReviewBody);
            //this gets the word count
            Dictionary<string, int> wordCount = getWordCountOfArray(wordArr);
            //this gets the review weight       
            ReviewWeight = getReviewWeight(wordCount, Rating);
        }
        
        
        //This creates an array for every word in the reivew, and make each lower case
        public string[] getArrayOfWords(string stringToSplit)
        {
            //makes everything in array lower case
            string review = stringToSplit.ToLower();
            //replaces apostrophes 
            string ratingString = Regex.Replace(review, @"[\']", "");
          
            char[] delimiters = new[] { ',', ';', ' ', '.' };
            //split the string up 
            var wordArr = review.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return wordArr;
        }

        //this counts the number of words in an array and puts the count as a value in a dictionary 
        public Dictionary<string, int> getWordCountOfArray(string[] wordArray)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int> { };

            for (int i = 0; i < wordArray.Length; i++)
            {
                int currentCount;
                //this checks if the word in the review is in the word count array 
                wordCount.TryGetValue(wordArray[i], out currentCount);
                //if it is found it increments the count by one if it is not found it adds it to the dictionary 
                wordCount[wordArray[i]] = currentCount + 1;
            }
            return wordCount;
        }

        //this gets the weight of a review by running the words against a weighted dictionary of reviews 
        public int getReviewWeight(Dictionary<string, int> wordCountDictionary, int intialWeight)
        {
            int reviewWeight = intialWeight;
            //this intiates a new wordcount object
            WordLists wordLists = new WordLists();
            foreach (var word in wordCountDictionary)
            {
                int currentCount;
                //this checks if the word in the review matches a word in the positve dictionary 
                wordLists.PositiveWords.TryGetValue(word.Key, out currentCount);
                //if it is found it multiplies the occurences in the review by the weight of the word
                reviewWeight += currentCount * word.Value;

                wordLists.NegativeWords.TryGetValue(word.Key, out currentCount);
                reviewWeight += currentCount * word.Value;
            }

           return reviewWeight;
        }
    }
    
}
