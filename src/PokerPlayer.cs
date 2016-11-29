using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "1.0.5";

        public static int BetRequest(JObject gameState)
        {
            //TODO: Use this method to return the value You want to bet
            var state = gameState.ToObject<MainState>();
            var player = state.players[state.in_action];


            var card1 = player.hole_cards[0];
            var card2 = player.hole_cards[1];

            if (state.community_cards.Any())
            {
                var totalPoints = HandAnalyzer.TotalPoints(card1, card2, state.community_cards);
                if (totalPoints > 400)
                {
                    return state.current_buy_in - player.bet + state.minimum_raise * 8;
                }

            }
            int limit = 140;
            int highBetlimit = 300;

            var cardPoints = CardAnalyzer.GetPoints(card1, card2);

            if (300 < state.round)
            {
                limit = 120;
            }
            else if (500 < state.round)
            {
                limit = 100;
            }
            else if (700 < state.round)
            {
                limit = 40;
            }

            if (state.players.Count(x => String.Equals(x.status, "active", StringComparison.InvariantCultureIgnoreCase)) == 2 && state.round > 2000 && player.bet > 500)
            {
                limit = 0;
            }

            limit = limit * (1 + state.bet_index / 100);
            highBetlimit = highBetlimit * (1 + state.bet_index / 100);


            if (100 == CardAnalyzer.HighPair(card1, card2))
            {
                return state.current_buy_in - player.bet + state.minimum_raise * 8;
            }

            if (highBetlimit < cardPoints)
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
    }
}

