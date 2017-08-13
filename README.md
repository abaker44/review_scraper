Installation instructions
```
On a windows machine 
1. git clone https://github.com/abaker44/review_scraper.git
2. run reiview_scraper\mckaig_chevy_review\bin\Debug\mckaig_chevy_review.exe
```

This application prints the top three most positive results found at "http://www.dealerrater.com/dealer/McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685/" on the first five pages. it then prints out all of the reviews to a file called reviews in the folder of the executible. You can change the number of pages the program scrapes and how many reviews are printed out by changing the "pagesToGet" and the "reviewsToPrint" variables in the Program.cs file. The program calculates how positive a review is by counting each word in the review and assigning it a weight. the total weight for each word is add and assigned to the review. The weights for each word is assigned in the WordLists.cs class. Word can be add to the dictionary with the word as the key and the value is the weight associated with the word. adding more words and adjusting the weights of the words will provide more accurate results. 
