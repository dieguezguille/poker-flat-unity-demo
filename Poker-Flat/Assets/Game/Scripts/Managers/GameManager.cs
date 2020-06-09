﻿using DG.Tweening;

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject _deck;
	public GameObject _cardLocators;
	public GameObject _world;

	public List<CardController> Cards;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		DeckManager.Instance.Init();
		DeckManager.Instance.GetCards(5);
		InstantiateCards();
	}

	private void InstantiateCards()
	{
		var deck = DeckManager.Instance;

		if (deck.DealtCards != null && deck.DealtCards.Count > 0)
		{
			for (int i = 0; i < deck.DealtCards.Count; i++)
			{
				var card = deck.DealtCards[i];
				var cardGo = Instantiate(Globals.CardPrefab);
				cardGo.transform.SetParent(_world.transform);
				Vector3 deckPos = DeckManager.Instance.Deck.transform.position;
				cardGo.transform.position = deckPos;
				CardController controller = cardGo.GetComponent<CardController>();
				Cards.Add(controller);
				controller.SetValues(card);
				controller._initialPos = _cardLocators.transform.GetChild(i).position;
				controller.MoveTo(controller._initialPos, 1.5f);
			}
		}
	}

	public void ChangeCards()
	{
		List<CardController> selectedCards = Cards.FindAll(cardController => cardController._card.IsSelected);

		foreach (var cardController in selectedCards)
		{
			cardController.Replace();
		}
	}
}
