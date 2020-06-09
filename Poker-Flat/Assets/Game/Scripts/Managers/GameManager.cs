using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject _deck;
	public GameObject _cardLocators;
	public GameObject _world;

	public List<GameObject> Cards;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		DeckManager.Instance.Init();
		DeckManager.Instance.GetCards();

		InstantiateCards();
	}

	public void InstantiateCards()
	{
		var deck = DeckManager.Instance;

		if (deck.DealtCards != null && deck.DealtCards.Count > 0)
		{
			for (int i = 0; i < deck.DealtCards.Count; i++)
			{
				var cardModel = deck.DealtCards[i];

				var cardGo = Instantiate(Globals.CardPrefab);
				cardGo.transform.SetParent(_world.transform);
				cardGo.transform.position = DeckManager.Instance.Deck.position;
				CardController controller = cardGo.GetComponent<CardController>();
				controller.SetValues(cardModel);
				controller._initialPos = _cardLocators.transform.GetChild(i).position;
				controller.MoveTo(controller._initialPos, 0.7f);

				Cards.Add(cardGo);
			}
		}
	}

	public void ChangeCards()
	{
		List<GameObject> selectedCards = Cards.FindAll(card => card.GetComponent<CardController>().Model.IsSelected);

		foreach (var card in selectedCards)
		{
			card.GetComponent<CardController>().Replace();
		}
	}
}
