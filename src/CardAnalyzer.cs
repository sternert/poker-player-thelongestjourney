using System;

namespace Nancy.Simple
{
    public class CardAnalyzer
    {
        public static int CardPoints(HoleCard card)
        {
            switch (card.rank)
            {
                case "A":
                    return 100;
                case "K":
                    return 60;
                case "Q":
                    return 40;
                case "J":
                    return 20;
                case "10":
                    return 10;
                case "9":
                    return 9;
                case "8":
                    return 8;
                case "7":
                    return 7;
                case "6":
                    return 6;
                case "5":
                    return 5;
                case "4":
                    return 4;
                case "3":
                    return 3;
                case "2":
                    return 2;
                case "1":
                    return 1;
                default:
                    return 0;
            }
        }

        public static int GetPoints(HoleCard card1, HoleCard card2)
        {
            int points = 0;
            points += Highcard(card1, card2);
            points += HighPair(card1, card2);

            if (IsPair(card1, card2))
            {
                points += 80;
            }

            if (IsSuit(card1, card2))
            {
                points += 15;
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
