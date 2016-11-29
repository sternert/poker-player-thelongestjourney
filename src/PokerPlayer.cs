using System;
using System.Collections.Generic;
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

            double limit = 140;

            var cardPoints = CardAnalyzer.GetPoints(card1, card2);

            if (state.small_blind * 2 < 5)
            {
                limit = 120;
            }
            else if (state.small_blind * 2 < 9)
            {
                limit = 110;
                if (player.bet == 0)
                {
                    limit += 10;
                }
            }
            else if (state.small_blind * 2 < 14)
            {
                limit = 80;
                if (player.bet == 0)
                {
                    limit += 10;
                }
            }
            else if (state.small_blind * 2 < 19)
            {
                limit = 60;
                if (player.bet == 0)
                {
                    limit += 20;
                }
            }
            else
            {
                limit = 40;
                if (player.bet == 0)
                {
                    limit += 26;
                }
            }

            if (100 == CardAnalyzer.HighPair(card1, card2))
            {
                return player.stack;
            }

            if (numberOfActivePlayers(state.players) == 2)
            {
                var otherPlayer = state.players.FirstOrDefault(other => other.id != player.id && other.status == "active");
                if (player.stack + player.bet > (1.2 * (otherPlayer.stack + otherPlayer.bet)))
                {
                    limit = limit*0.6;
                }
                else if (otherPlayer.name == "KarinDaniel")
                {
                    if (otherPlayer.bet <= state.small_blind * 2)
                    {
                        limit = limit*0.3;
                    }
                    else if (otherPlayer.stack == 0 && player.stack >= 1000)
                    {
                        limit = limit*1.6;
                    }
                }
            }
            if ((player.stack + player.bet) < state.small_blind * 4)
            {
                limit = 0;
            }

            if (limit < cardPoints)
            {
                return (state.pot + 15) * 2;
            }

            return 0;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }

        public static int numberOfActivePlayers(List<Player> players)
        {
            return players.Count(player => player.status == "active");
        }
    }
}
