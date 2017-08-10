using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        private void CalculateReviewWeight()
        {
            int reviewWeight = Rating;
           Dictionary<string, int> wordCount= new Dictionary<string, int> { };
            WordLists wordLists = new WordLists();
            string review = ReviewBody.ToLower() + " " + ReviewTitle.ToLower();
            char[] delimiters = new[] { ',', ';', ' ', '.' };  
            var wordArr = review.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < wordArr.Length; i++)
            {
                int currentCount;
         
                wordCount.TryGetValue(wordArr[i], out currentCount);
        
                wordCount[wordArr[i]] = currentCount + 1;
            }
            foreach (var item in wordCount)
            {
                int currentCount;

                wordLists.PositiveWords.TryGetValue(item.Key, out currentCount);
                reviewWeight += currentCount * item.Value;

                wordLists.NegativeWords.TryGetValue(item.Key, out currentCount);
                reviewWeight += currentCount * item.Value;
            }

            ReviewWeight = reviewWeight;
        }
    }
}
