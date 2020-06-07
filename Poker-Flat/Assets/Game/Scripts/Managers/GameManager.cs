using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject _cardDeck;
	public GameObject _cardHolders;

	GameObject cardPrefab;
	List<CardModel> Cards;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		cardPrefab = Resources.Load("Prefabs/Cards/Card") as GameObject;
		Cards = new List<CardModel>();
		DeckManager.Instance.Init();
		
		Cards = DeckManager.Instance.GetCards(5);
		DeckManager.Instance.RetrieveCard(Cards[1]);
		InstantiateCards();
	}

	private void InstantiateCards()
	{
		if (Cards != null && Cards.Count > 0)
		{
			
			for (int i = 0; i < _cardHolders.transform.childCount; i++)
			{
				var gameObject = Instantiate(cardPrefab);
				gameObject.GetComponent<CardController>().SetValues(Cards[i]);
				gameObject.transform.position = _cardHolders.transform.GetChild(i).position;
			}
		}
	}
}
