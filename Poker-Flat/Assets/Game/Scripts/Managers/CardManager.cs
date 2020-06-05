using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class CardManager
{
	private static CardManager instance;
	public static CardManager Instance
	{
        get
        {
            if (instance == null)
            {
                instance = new CardManager();
            }

            return instance;
        }
    }
	public OrderedDictionary AllCards { get; set; }

	private CardManager()
	{

	}

    public bool LoadCards()
	{
		bool cardsLoaded = false;

		try
		{
			Texture2D[] loadedTextures = Resources.LoadAll<Texture2D>("Sprites/Cards/Front");

			AllCards = new OrderedDictionary();

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

					AllCards.Add(card, texture);
				}
			}

			return cardsLoaded = AllCards.Count == 52;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			return cardsLoaded;
		}
	}

	public List<CardModel> DealCards()
	{
		HashSet<CardModel> cards = null;

		try
		{
			if (AllCards != null && AllCards.Count > 0)
			{
				cards = new HashSet<CardModel>();

				var random = new System.Random();

				while (cards.Count < 5)
				{
					int index = random.Next(0, AllCards.Count - 1);
					cards.Add((CardModel)AllCards.Cast<DictionaryEntry>().ElementAt(index).Key);
				}
			}

			return cards.ToList();
		}
		catch(Exception ex)
		{
			Debug.LogError(ex);
			return cards.ToList();
		}
	}
}
