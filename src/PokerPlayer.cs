using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "1.0.1";

        public static int BetRequest(JObject gameState)
        {
            //TODO: Use this method to return the value You want to bet
            var state = gameState.ToObject<MainState>();
            var player = state.players[state.in_action];


            var card1 = player.hole_cards[0];
            var card2 = player.hole_cards[1];

            if (200 < state.round)
            {
                if (60 < highcard(card1, card2))
                {
                    return state.current_buy_in - player.bet + state.minimum_raise;
                }

                if (card1.rank == card2.rank)
                {
                    return state.current_buy_in - player.bet + state.minimum_raise * 4;
                }
            }
            else
            {
                if (100 < highcard(card1, card2))
                {
                    return state.current_buy_in - player.bet + state.minimum_raise;
                }

                if (card1.rank == card2.rank)
                {
                    return state.current_buy_in - player.bet + state.minimum_raise * 4;
                }
            }


            return 0;
        }

        private static int CardPoints(HoleCard card)
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

        private static int highcard(HoleCard card1, HoleCard card2)
        {
            int points = 0;


            points += CardPoints(card1);
            points += CardPoints(card2);

            if (card1.suit == card2.suit)
            {
                points += 50;
            }

            return points;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}

