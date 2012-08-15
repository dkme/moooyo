using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace CBB.RecommendationHelper.SlopeOne
{
    public class Rating
    {
        public float Value { get; set; }
        public int Freq { get; set; }

        public float AverageValue
        {
            get { return Value / Freq; }
        }
    }

    public class RatingDifferenceCollection : Dictionary<string, Rating>
    {
        private string GetKey(string Item1Id, string Item2Id)
        {
            return (ObjectId.Parse(Item1Id) < ObjectId.Parse(Item2Id)) ? Item1Id + "/" + Item2Id : Item2Id + "/" + Item1Id ;
        }

        public bool Contains(string Item1Id, string Item2Id)
        {
            return this.Keys.Contains<string>(GetKey(Item1Id, Item2Id));
        }

        public Rating this[string Item1Id, string Item2Id]
        {
            get {
                    return this[this.GetKey(Item1Id, Item2Id)];
            }
            set { this[this.GetKey(Item1Id, Item2Id)] = value; }
        }
    }

    public class SlopeOne
    {        
        public RatingDifferenceCollection _DiffMarix = new RatingDifferenceCollection();  // The dictionary to keep the diff matrix
        public HashSet<string> _Items = new HashSet<string>();  // Tracking how many items totally

        public void AddUserRatings(IDictionary<string, float> userRatings)
        {
            foreach (var item1 in userRatings)
            {
                string item1Id = item1.Key;
                float item1Rating = item1.Value;
                _Items.Add(item1.Key);

                foreach (var item2 in userRatings)
                {
                    if (ObjectId.Parse(item2.Key) <= ObjectId.Parse(item1Id)) continue; // Eliminate redundancy
                    string item2Id = item2.Key;
                    float item2Rating = item2.Value;

                    Rating ratingDiff;
                    if (_DiffMarix.Contains(item1Id, item2Id))
                    {
                        ratingDiff = _DiffMarix[item1Id, item2Id];
                    }
                    else
                    {
                        ratingDiff = new Rating();
                        _DiffMarix[item1Id, item2Id] = ratingDiff;
                    }

                    ratingDiff.Value += item1Rating - item2Rating;
                    ratingDiff.Freq += 1;
                }
            }
        }

        // Input ratings of all users
        public void AddUerRatings(IList<IDictionary<string, float>> Ratings)
        {
            foreach(var userRatings in Ratings)
            {
                AddUserRatings(userRatings);
            }
        }

        public IDictionary<string, float> Predict(IDictionary<string, float> userRatings)
        {
            Dictionary<string, float> Predictions = new Dictionary<string, float>();
            foreach (var itemId in this._Items)
            {
                if (userRatings.Keys.Contains(itemId))    continue; // User has rated this item, just skip it

                Rating itemRating = new Rating();

                foreach (var userRating in userRatings)
                {
                    if (userRating.Key == itemId) continue;
                    string inputItemId = userRating.Key;
                    if (_DiffMarix.Contains(itemId, inputItemId))
                    {
                        Rating diff = _DiffMarix[itemId, inputItemId];
                        itemRating.Value += diff.Freq * (userRating.Value + diff.AverageValue * ((ObjectId.Parse(itemId) < ObjectId.Parse(inputItemId)) ? 1 : -1));
                        itemRating.Freq += diff.Freq;
                    }
                }
                Predictions.Add(itemId, itemRating.AverageValue);                
            }
            return Predictions;
        }
        /*
        public static void Test()
        {
            SlopeOne test = new SlopeOne();

            Dictionary<int, float> userRating = new Dictionary<int, float>();
            userRating.Add(1, 5);
            userRating.Add(2, 4);
            userRating.Add(3, 4);
            test.AddUserRatings(userRating);

            userRating = new Dictionary<int, float>();
            userRating.Add(1, 4);
            userRating.Add(2, 5);
            userRating.Add(3, 3);
            userRating.Add(4, 5);
            test.AddUserRatings(userRating);

            userRating = new Dictionary<int, float>();
            userRating.Add(1, 4);
            userRating.Add(2, 4);
            userRating.Add(4, 5);
            test.AddUserRatings(userRating);

            userRating = new Dictionary<int, float>();
            userRating.Add(1, 5);
            userRating.Add(3, 4);

            IDictionary<int, float> Predictions = test.Predict(userRating);
            foreach (var rating in Predictions)
            {
                Console.WriteLine("Item " + rating.Key + " Rating: " + rating.Value);
            }
        }
        */
    }
}
