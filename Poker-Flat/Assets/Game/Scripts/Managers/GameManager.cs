using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
	GameObject cardPrefab;
	List<Card> Cards;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		cardPrefab = Resources.Load("Prefabs/Cards/Card") as GameObject;
		Cards = new List<Card>();
		Deck.Instance.Init();
		
		//Cards = Deck.Instance.GetCards(5);
		//Deck.Instance.RetrieveCard(Cards[1]);
		//DealCards();
	}

	private void InstantiateCards()
	{
		if (Cards != null && Cards.Count > 0)
		{
			foreach (var card in Cards)
			{
				var gameObject = Instantiate(cardPrefab);
				gameObject.GetComponent<CardController>().SetValues(card);
			}
		}
	}
}
