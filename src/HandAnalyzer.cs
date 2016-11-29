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

            var rankGroup = cardList.GroupBy(group => group.rank).OrderByDescending(group => group.Count()).ToArray();
            var suitGroup = cardList.GroupBy(group => group.suit).OrderByDescending(group => group.Count()).ToArray();

            if (FourOfAKind(rankGroup))
            {
                Console.Error.WriteLine("FourOfAKind!");
                return 1000;
            }

            if (FullHouse(rankGroup))
            {
                Console.Error.WriteLine("FullHouse!");
                return 800;
            }

            if (Flush(suitGroup))
            {
                Console.Error.WriteLine("Flush!");
                return 600;
            }

            if (ThreeOfAKind(rankGroup))
            {
                Console.Error.WriteLine("ThreeOfAKind!");
                return 600;
            }

            if (TwoOfAKind(rankGroup))
            {
                Console.Error.WriteLine("TwoOfAKind!");
                return 400;
            }

            return 0;
        }

        private static bool TwoOfAKind(IGrouping<string, HoleCard>[] rankGrouped)
        {
            var highest = rankGrouped[0];
            var second = rankGrouped[1];
            return highest.Count() == 2 && second.Count() == 2;
        }

        private static bool FullHouse(IGrouping<string, HoleCard>[] rankGrouped)
        {
            var highest = rankGrouped[0];
            var second = rankGrouped[1];
            return highest.Count() == 3 && second.Count() == 2;
        }

        private static bool ThreeOfAKind(IGrouping<string, HoleCard>[] rankGrouped)
        {
            var highest = rankGrouped[0];
            return highest.Count() == 3;
        }

        private static bool FourOfAKind(IGrouping<string, HoleCard>[] rankGrouped)
        {
            var highest = rankGrouped[0];
            return highest.Count() == 4;
        }

        private static bool Flush(IGrouping<string, HoleCard>[] suitGrouped)
        {
            return suitGrouped.First().Count() >= 5;
        }
    }
}
