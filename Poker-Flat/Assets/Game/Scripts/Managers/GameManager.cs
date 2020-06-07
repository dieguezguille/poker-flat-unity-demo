using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject _cardDeck;
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
			foreach (var card in Cards)
			{
				var gameObject = Instantiate(cardPrefab);
				gameObject.GetComponent<CardController>().SetValues(card);
				gameObject.transform.localPosition = new Vector3(_cardDeck.transform.localPosition.x, _cardDeck.transform.localPosition.y + .1f, _cardDeck.transform.localPosition.z);
			}
		}
	}
}
