using Assets.Game.Scripts.Support;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class DeckManager
{
	private static DeckManager instance;
	public static DeckManager Instance
	{
        get
        {
            if (instance == null)
            {
                instance = new DeckManager();
            }

            return instance;
        }
    }

	public List<CardModel> Cards { get; set; }
	public List<CardModel> DealtCards { get; set; }
	private GameObject Deck { get; set; }
	private System.Random _random { get; set; }

	private DeckManager()
	{

	}

	public void Init()
	{
		_random = new System.Random();
		Cards = new List<CardModel>();
		DealtCards = new List<CardModel>();
		Deck = GameObject.Find("Deck");

		Globals.CardPrefab = Resources.Load(ConfigValues.CardPrefab) as GameObject;
		Globals.CardTextures = Resources.LoadAll<Texture2D>(ConfigValues.FrontFaceTextures);
		Globals.DeckTexture = Resources.Load<Texture2D>(string.Format(ConfigValues.BackFaceTexture, ConfigValues.DeckColor.ToString())); ;

		LoadDeck();
	}

    private void LoadDeck()
	{
		try
		{
			foreach (var texture in Globals.CardTextures)
			{
				string[] attributes = texture.name.Split('-');

				bool parsedColor = Enum.TryParse(attributes[0], out CardColor color);
				bool parsedSuit = Enum.TryParse(attributes[1], out CardSuit suit);
				bool parsedValue = int.TryParse(attributes[2], out int value);

				if (parsedColor && parsedSuit && parsedValue)
				{
					var card = new CardModel();

					card.Color = color;
					card.Suit = suit;
					card.Value = value;
					card.FrontTexture = texture;
					card.DeckTexture = Globals.DeckTexture;
					card.IsSelected = false;

					Cards.Add(card);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	public List<CardModel> GetRandomCards(int number)
	{
		var cards = new List<CardModel>();

		try
		{
			if (Cards != null && number <= Cards.Count)
			{
				while (DealtCards.Count < number)
				{
					cards.Add(GetRandomCard());
				}
			}

			return cards;
		}
		catch(Exception ex)
		{
			Debug.LogError(ex);
			return cards;
		}
	}

	public CardModel GetRandomCard()
	{
		int index = _random.Next(Cards.Count);

		DealtCards.Add(Cards[index]);
		Cards.RemoveAt(index);

		return DealtCards.Last();
	}

	public void ChangeCard(CardModel card)
	{
		if (DealtCards.Contains(card))
		{
			card.IsSelected = false;
			Cards.Add(card);
			DealtCards.Remove(card);
		}

		card.Controller.StartCoroutine(card.Controller.MoveTo(new Vector3(Deck.transform.position.x, Deck.transform.position.y + .25f, Deck.transform.position.z), 2f));
	}
}