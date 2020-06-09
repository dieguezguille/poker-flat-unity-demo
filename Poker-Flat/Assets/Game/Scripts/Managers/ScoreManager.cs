﻿using Assets.Game.Scripts.Enums;
using System.Linq;

namespace Assets.Game.Scripts.Managers
{
	public class ScoreManager
	{
		private static ScoreManager instance;
		public static ScoreManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ScoreManager();
				}

				return instance;
			}
		}

		private ScoreManager()
		{
		}

		public HandType CheckScore()
		{
			var cards = DeckManager.Instance.DealtCards;

			// check for poker
			var repeatingCardsByRank = cards
				.GroupBy(card => card.Rank)
				.Where(rank => rank.Count() > 1)
				.Select(element => new { Rank = element.Key, Count = element.Count() })
				.ToList();

			if (repeatingCardsByRank.Any(card => card.Count >= 4))
			{
				return HandType.Poker;
			}

			// check for straight
			var cardsByRank = cards
				.OrderBy(card => card.Rank)
				.ToList();

			bool isConsecutive = !cardsByRank.Select((i, j) => i.Rank - j).Distinct().Skip(1).Any();

			if (isConsecutive)
			{
				return HandType.Straight;
			}

			// check for three of a kind
			if (repeatingCardsByRank.Any(card => card.Count == 3))
			{
				return HandType.ThreeOfAKind;
			}

			// check for two pairs
			if (repeatingCardsByRank.Any(card => card.Count == 2))
			{
				var twoPairs = repeatingCardsByRank.FindAll(rank => rank.Count > 1);

				if (twoPairs.Count > 1)
				{
					return HandType.TwoPair;
				}
				else if (twoPairs.Count == 1)
				{
					return HandType.Pair;
				}
				else
				{
					return HandType.Other;
				}
			}
			else
			{
				return HandType.Other;
			}
		}
	}
}
