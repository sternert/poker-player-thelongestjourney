using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    public class HandAnalyzer
    {

        public static int TotalPoints(HoleCard handCard1, HoleCard handCard2, List<CommunityCard> communityCards)
        {
            var cardList = new List<HoleCard>();
            cardList.Add(handCard1);
            cardList.Add(handCard2);

            foreach (var communityCard in communityCards)
            {
                var holeCard = new HoleCard();
                holeCard.rank = communityCard.rank;
                holeCard.suit = communityCard.suit;
                cardList.Add(holeCard);
            }

            if (FourOfAKind(cardList))
            {
                Console.Error.WriteLine("FourOfAKind!");
                return 1000;
            }

            if (FullHouse(cardList))
            {
                Console.Error.WriteLine("FullHouse!");
                return 800;
            }

            if (Flush(cardList))
            {
                Console.Error.WriteLine("Flush!");
                return 600;
            }

            if (ThreeOfAKind(cardList))
            {
                Console.Error.WriteLine("ThreeOfAKind!");
                return 600;
            }

            if (TwoOfAKind(cardList))
            {
                Console.Error.WriteLine("TwoOfAKind!");
                return 400;
            }

            return 0;
        }

        private static bool TwoOfAKind(List<HoleCard> cardList)
        {
            var grouped = cardList.GroupBy(group => group.rank);
            var highest = grouped.OrderByDescending(group => group.Count()).ToArray()[0];
            var second = grouped.OrderByDescending(group => group.Count()).ToArray()[1];
            return highest.Count() == 2 && second.Count() == 2;
        }

        private static bool FullHouse(List<HoleCard> cardList)
        {
            var grouped = cardList.GroupBy(group => group.rank);
            var highest = grouped.OrderByDescending(group => group.Count()).ToArray()[0];
            var second = grouped.OrderByDescending(group => group.Count()).ToArray()[1];
            return highest.Count() == 3 && second.Count() == 2;
        }

        private static bool ThreeOfAKind(List<HoleCard> cardList)
        {
            var grouped = cardList.GroupBy(group => group.rank);
            return grouped.OrderByDescending(group => group.Count()).First().Count() >= 3;
        }

        private static bool FourOfAKind(List<HoleCard> cardList)
        {
            var grouped = cardList.GroupBy(group => group.rank);
            return grouped.OrderByDescending(group => group.Count()).First().Count() >= 4;
        }

        private static bool Flush(List<HoleCard> cardList)
        {
            var grouped = cardList.GroupBy(group => group.suit);
            return grouped.OrderByDescending(group => group.Count()).First().Count() >= 5;
        }
    }
}
