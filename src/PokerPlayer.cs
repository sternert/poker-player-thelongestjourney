using System.Collections.Generic;
using System.Runtime.InteropServices;
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

            var totalPoints = TotalPoints(card1, card2, state.community_cards);
            int limit = 100;
            int highBetlimit = 300;

            var cardPoints = CardAnalyzer.GetPoints(card1, card2);

            if (200 < state.round)
            {
                limit = 60;
            }

            if (100 == CardAnalyzer.HighPair(card1, card2))
            {
                return state.current_buy_in - player.bet + state.minimum_raise * 8;
            }

            if (cardPoints > highBetlimit)
            {
                return state.current_buy_in - player.bet + state.minimum_raise * 4;
            }

            if (limit < cardPoints)
            {
                return state.current_buy_in - player.bet + state.minimum_raise;
            }

            return 0;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }

        private static int TotalPoints(HoleCard handCard1, HoleCard handCard2, List<CommunityCard> communityCards)
        {

            return 0;
        }
    }
}

