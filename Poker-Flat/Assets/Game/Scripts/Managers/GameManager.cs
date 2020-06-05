using System;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
	List<CardModel> DealtCards;

	private void Start()
	{
		Init();
		DealCards();
	}

	private void Init()
	{
		if (CardManager.Instance.LoadCards())
		{
			DealtCards = CardManager.Instance.DealCards();
		}
	}

	private void DealCards()
	{
		var prefab = Resources.Load("Prefabs/Cards/Card");

		foreach (var card in DealtCards)
		{
			var gameObject = Instantiate(prefab) as GameObject;
			gameObject.GetComponent<CardController>().SetValues(card);
		}
	}
}
