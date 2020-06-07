using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject _deck;
	public GameObject _cardLocators;
	public GameObject _world;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		DeckManager.Instance.Init();
		DeckManager.Instance.GetRandomCards(5);
		InstantiateCards();
	}

	public void FlipCard(CardModel card)
	{
		StartCoroutine(card.Controller.RotateZ(180));
	}

	private void InstantiateCards()
	{
		var deck = DeckManager.Instance;

		if (deck.DealtCards != null && deck.DealtCards.Count > 0)
		{
			for (int i = 0; i < _cardLocators.transform.childCount; i++)
			{
				var card = deck.DealtCards[i];

				card.GameObject = Instantiate(Globals.CardPrefab);
				card.GameObject.transform.SetParent(_world.transform);
				var pos = _deck.transform.position;
				card.GameObject.transform.position = new Vector3(pos.x, pos.y + .3f, pos.z);
				card.Controller = card.GameObject.GetComponent<CardController>();
				card.Controller.SetValues(deck.DealtCards[i]);
				StartCoroutine(card.Controller.MoveTo(_cardLocators.transform.GetChild(i).position, 2f));
			}
		}
	}
}
