using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mckaig_chevy_review
{
    

    public class WordLists
    {
       public WordLists()
        {
            PositiveWords = new Dictionary<string, int> {
                { "fantastic", 4 },
                { "great", 2 },
                { "amazing", 4 },
                { "awesome", 4 },
                { "cool", 2 },
                { "nice", 2 },
                { "super", 2 },
                { "superb", 4 },
                { "wow", 2 },
                { "helpful", 2 },
                { "love", 2 },
                { "listens", 2 },
                { "friendly", 3 },
                { "would", 2 },
                { "professional", 2 },
                { "polite", 2 },
                { "110", 2 },
                { "gold", 2 },
                { "thank", 2 },
                { "courteous", 2 },
                { "perfectly", 2 },
                { "excellent", 2 },
                { "beyond", 2 },
                { "best", 2 },
                { "pleasant", 2 },
                { "reccomend", 2 },
                { "helped", 2 },
                { "blessing", 2 },
                { "wonderful", 4 },
                { "fast", 2 },
                { "exact", 2 },
                { "done", 2 } };

            NegativeWords = new Dictionary<string, int> {
                { "terrible", -4 },
                { "awful", -4 },
                { "horrible", -4 },
                { "worst", -4 },
                { "hate", -4 },
                { "not", -2 },
                { "bad", -2 },
                { "aggressive", -2 },
                { "charge", -2 },
                { "no", -2 },
                { "told", -2 },
                { "refused", -2 },
                { "bbb", -2 },
                { "ethics", -2 },
                { "waste", -2 } };
        }
        
         public Dictionary<string, int> PositiveWords { get; }
         public Dictionary<string, int> NegativeWords { get; }
       
        
       

    }
}
