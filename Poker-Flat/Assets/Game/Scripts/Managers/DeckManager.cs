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

	private DeckManager()
	{

	}

	public void Init()
	{
		Cards = new List<CardModel>();
		DealtCards = new List<CardModel>();
		LoadDeck();
	}

    private bool LoadDeck()
	{
		bool cardsLoaded = false;

		try
		{
			Texture2D[] loadedTextures = Resources.LoadAll<Texture2D>("Sprites/Cards/Front");
			Texture2D backFaceTexture = Resources.Load<Texture2D>($"Sprites/Cards/Back/Card-Back-Blue");
			GlobalResources.BackFaceTexture = backFaceTexture;

			Cards = new List<CardModel>();

			foreach (var texture in loadedTextures)
			{
				string[] attributes = texture.name.Split('-');

				var card = new CardModel();

				bool parsedColor = Enum.TryParse(attributes[0], out CardColor color);
				bool parsedSuit = Enum.TryParse(attributes[1], out CardSuit suit);
				bool parsedValue = int.TryParse(attributes[2], out int value);

				if (parsedColor && parsedSuit && parsedValue)
				{
					card.Color = color;
					card.Suit = suit;
					card.Value = value;
					card.FrontFaceTexture = texture;
					card.BackFaceTexture = backFaceTexture;

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

	public List<CardModel> GetCards(int number)
	{
		var cards = new List<CardModel>();

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

	public CardModel GetCard()
	{
		var random = new System.Random();
		int index = random.Next(0, Cards.Count - 1);

		DealtCards.Add(Cards[index]);
		Cards.RemoveAt(index);

		return DealtCards.Last();
	}

	public void RetrieveCard(CardModel card)
	{
		if (DealtCards.Contains(card))
		{
			Cards.Add(card);
			DealtCards.Remove(card);
		}
	}
}
