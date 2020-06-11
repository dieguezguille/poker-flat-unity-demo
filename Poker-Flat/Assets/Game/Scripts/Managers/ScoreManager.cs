using Assets.Game.Scripts.Enums;
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

			//foreach (var card in cards)
			//{
			//	UnityEngine.Debug.Log($"{card.Rank} - {card.Suit}");
			//}

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
			if (repeatingCardsByRank.Any(repeatingCard => repeatingCard.Count == 3))
			{
				return HandType.ThreeOfAKind;
			}

			// check for two pairs
			var pairs = repeatingCardsByRank.FindAll(card => card.Count == 2);

			if (pairs.Count > 0)
			{
				if (pairs.Count == 2)
				{
					return HandType.TwoPair;
				}
				else if (pairs.Count == 1)
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
