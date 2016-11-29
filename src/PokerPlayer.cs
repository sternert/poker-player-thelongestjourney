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

            int limit = 100;

            if (200 < state.round)
            {
                limit = 60;
            }

            if (200 == CardAnalyzer.Highcard(card1, card2))
            {
                return state.pot;
            }

            if (limit < CardAnalyzer.Highcard(card1, card2))
            {
                return state.current_buy_in - player.bet + state.minimum_raise;
            }

            if (CardAnalyzer.IsPair(card1, card2))
            {
                return state.current_buy_in - player.bet + state.minimum_raise * 4;
            }

            return 0;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}

