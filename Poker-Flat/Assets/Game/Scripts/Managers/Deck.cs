using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Deck
{
	private static Deck instance;
	public static Deck Instance
	{
        get
        {
            if (instance == null)
            {
                instance = new Deck();
            }

            return instance;
        }
    }

	public List<Card> Cards { get; set; }
	public List<Card> DealtCards { get; set; }

	private Deck()
	{

	}

	public void Init()
	{
		Cards = new List<Card>();
		DealtCards = new List<Card>();
		LoadDeck();
	}

    private bool LoadDeck()
	{
		bool cardsLoaded = false;

		try
		{
			Texture2D[] loadedTextures = Resources.LoadAll<Texture2D>("Sprites/Cards/Front");
			Texture2D backfaceTexture = Resources.Load<Texture2D>($"Sprites/Cards/Back/Card-Back-Blue");

			Cards = new List<Card>();

			foreach (var texture in loadedTextures)
			{
				string[] attributes = texture.name.Split('-');

				var card = new Card();

				bool parsedColor = Enum.TryParse(attributes[0], out CardColor color);
				bool parsedSuit = Enum.TryParse(attributes[1], out CardSuit suit);
				bool parsedValue = int.TryParse(attributes[2], out int value);

				if (parsedColor && parsedSuit && parsedValue)
				{
					card.Color = color;
					card.Suit = suit;
					card.Value = value;
					card.FrontFaceTexture = texture;
					card.BackFaceTexture = backfaceTexture;

					Cards.Add(card);
				}
			}

			return cardsLoaded = Cards.Count == loadedTextures.Length;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			return cardsLoaded;
		}
	}

	public List<Card> GetCards(int number)
	{
		var cards = new List<Card>();

		try
		{
			if (Cards != null && number <= Cards.Count)
			{
				while (DealtCards.Count < number)
				{
					cards.Add(GetCard());
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

	public Card GetCard()
	{
		var random = new System.Random();
		int index = random.Next(0, Cards.Count - 1);

		DealtCards.Add(Cards[index]);
		Cards.RemoveAt(index);

		return DealtCards.Last();
	}

	public void RetrieveCard(Card card)
	{
		if (DealtCards.Contains(card))
		{
			Cards.Add(card);
			DealtCards.Remove(card);
		}
	}
}
