using Assets.Game.Scripts.Enums;
using Assets.Game.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[HideInInspector]
	public List<GameObject> Cards;

	public GameObject _deck;
	public GameObject _cardLocators;
	public GameObject _world;

	public Button _changeCardsButton;
	public Button _tryAgainButton;

	public Text _scoreText;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		DeckManager.Instance.Init();
		DeckManager.Instance.GetCards();

		_tryAgainButton.gameObject.SetActive(false);
		_scoreText.gameObject.SetActive(false);

		StartCoroutine(InstantiateCardsCoroutine());
	}
	public void ChangeCards()
	{
		StartCoroutine(ChangeCardsCoroutine());
	}

	public void Restart()
	{
		StartCoroutine(RestartCoroutine());
	}

	IEnumerator InstantiateCardsCoroutine()
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
				yield return null;
			}
		}

		yield return new WaitForSeconds(1f);
		CheckScore();
	}

	IEnumerator ChangeCardsCoroutine()
	{
		List<GameObject> selectedCards = Cards.FindAll(card => card.GetComponent<CardController>().Model.IsSelected);

		if (selectedCards.Count > 0)
		{
			foreach (var card in selectedCards)
			{
				card.GetComponent<CardController>().Replace();
				yield return null;
			}

			_scoreText.gameObject.SetActive(false);
			_changeCardsButton.interactable = false;
			_tryAgainButton.gameObject.SetActive(true);

			yield return new WaitForSeconds(3f);
			CheckScore();
		}
	}

	IEnumerator RestartCoroutine()
	{
		foreach (var card in Cards)
		{
			card.GetComponent<CardController>().Replace();
			yield return null;
		}

		_tryAgainButton.gameObject.SetActive(false);
		_changeCardsButton.interactable = true;
		_scoreText.gameObject.SetActive(false);

		yield return new WaitForSeconds(3f);
		CheckScore();
	}

	private void CheckScore()
	{
		HandType hand = ScoreManager.Instance.CheckScore();
		_scoreText.gameObject.SetActive(true);
		_scoreText.text = $"{hand}: {(int)hand} points!";
	}
}
