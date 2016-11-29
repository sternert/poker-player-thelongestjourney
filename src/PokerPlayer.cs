using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "1.0.6";

        public static int BetRequest(JObject gameState)
        {
            //TODO: Use this method to return the value You want to bet
            var state = gameState.ToObject<MainState>();
            var player = state.players[state.in_action];

            var card1 = player.hole_cards[0];
            var card2 = player.hole_cards[1];

            if (state.community_cards.Any())
            {
                Console.Error.WriteLine("CommunityCards!");
                //var totalPoints = HandAnalyzer.TotalPoints(card1, card2, state.community_cards);
                //if (totalPoints > 400)
                //{
                //    return state.current_buy_in - player.bet + state.minimum_raise * 8;
                //}

            }
            double limit = 140;
            int highBetlimit = 300;

            var cardPoints = CardAnalyzer.GetPoints(card1, card2);

            if (5 > state.minimum_raise)
            {
                limit = 120;
            }
            else if (7 > state.minimum_raise)
            {
                limit = 100;
                if (player.bet == 0)
                {
                    limit += 10;
                }
            }
            else if (9 > state.minimum_raise)
            {
                limit = 40;
                if (player.bet == 0)
                {
                    limit += 10;
                }
            }
            else if (17 > state.minimum_raise)
            {
                limit = 20;
                if (player.bet == 0)
                {
                    limit += 20;
                }
            }
            else
            {
                limit = 10;
                if (player.bet == 0)
                {
                    limit += 26;
                }
            }

            if (100 == CardAnalyzer.HighPair(card1, card2))
            {
                return state.pot + 30;
            }

            if (highBetlimit < cardPoints)
            {
                return state.pot + 30;
            }

            if (limit < cardPoints)
            {
                return state.pot + 30;
            }

            return 0;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}

