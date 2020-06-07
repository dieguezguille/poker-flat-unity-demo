using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject _cardDeck;
	public GameObject _cardHolders;
	public GameObject _world;

	List<CardModel> Cards;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		Cards = new List<CardModel>();
		DeckManager.Instance.Init();
		Cards = DeckManager.Instance.GetRandomCards(5);
		InstantiateCards();
		FlipCards();
	}

	private void FlipCards()
	{
		//foreach (var card in Cards)
		//{
		//	StartCoroutine(card)
		//}
	}

	private void InstantiateCards()
	{
		if (Cards != null && Cards.Count > 0)
		{
			for (int i = 0; i < _cardHolders.transform.childCount; i++)
			{
				var gameObject = Instantiate(Globals.CardPrefab);
				gameObject.transform.SetParent(_world.transform);

				var card = gameObject.GetComponent<CardController>();
				card.SetValues(Cards[i]);

				var pos = _cardDeck.transform.position;
				gameObject.transform.position = new Vector3(pos.x, pos.y + 2f, pos.z);

				StartCoroutine(card.MoveTo(_cardHolders.transform.GetChild(i).position));
			}
		}
	}
}
