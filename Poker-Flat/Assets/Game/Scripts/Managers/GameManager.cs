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
		DeckManager.Instance.Init();
		Cards = DeckManager.Instance.GetRandomCards(5);
		InstantiateCards();
	}

	public void FlipCard(CardModel card)
	{
		StartCoroutine(card.Controller.RotateZ(180));
	}

	private void InstantiateCards()
	{
		if (Cards != null && Cards.Count > 0)
		{
			for (int i = 0; i < _cardHolders.transform.childCount; i++)
			{
				var card = Cards[i];

				card.GameObject = Instantiate(Globals.CardPrefab);
				card.GameObject.transform.SetParent(_world.transform);

				card.Controller = card.GameObject.GetComponent<CardController>();
				card.Controller.SetValues(Cards[i]);

				var pos = _cardDeck.transform.position;
				card.GameObject.transform.position = new Vector3(pos.x, pos.y + 2f, pos.z);
				StartCoroutine(card.Controller.MoveTo(_cardHolders.transform.GetChild(i).position));
			}
		}
	}
}
