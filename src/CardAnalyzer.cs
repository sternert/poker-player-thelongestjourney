using System;

namespace Nancy.Simple
{
    public class CardAnalyzer
    {
        public static int CardPoints(HoleCard card)
        {
            if (card.rank == "A")
            {
                return 100;
            }

            if (card.rank == "K")
            {
                return 60;
            }

            if (card.rank == "Q")
            {
                return 40;
            }

            if (card.rank == "J")
            {
                return 20;
            }

            if (card.rank == "10")
            {
                return 10;
            }

            return 0;
        }

        public static int GetPoints(HoleCard card1, HoleCard card2)
        {
            int points = 0;
            points += Highcard(card1, card2);
            points += HighPair(card1, card2);

            if (IsPair(card1, card2))
            {
                points += 60;
            }

            if (IsSuit(card1, card2))
            {
                points += 20;
            }

            return points;
        }

        public static int Highcard(HoleCard card1, HoleCard card2)
        {
            int points = 0;

            points += CardPoints(card1);
            points += CardPoints(card2);

            return points;
        }

        public static int HighPair(HoleCard card1, HoleCard card2)
        {
            if (!IsPair(card1, card2))
            {
                return 0;
            }

            if (card1.rank == "A" || card1.rank == "K" || card1.rank == "Q")
            {
                return 100;
            }

            return 0;
        }

        public static Boolean IsPair(HoleCard card1, HoleCard card2)
        {
            return card1.rank == card2.rank;
        }

        public static Boolean IsSuit(HoleCard card1, HoleCard card2)
        {
            return card1.suit == card2.suit;
        }
    }
}
