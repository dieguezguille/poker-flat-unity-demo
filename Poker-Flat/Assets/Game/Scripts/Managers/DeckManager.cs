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
	public Transform Deck { get; set; }
	private System.Random Random { get; set; }

	private DeckManager()
	{
	}

	public void Init()
	{
		Cards = new List<CardModel>();
		DealtCards = new List<CardModel>();
		Random = new System.Random();
		Deck = GameObject.Find("DeckLocator").transform;

		Globals.CardPrefab = Resources.Load(ConfigValues.CardPrefab) as GameObject;
		Globals.CardTextures = Resources.LoadAll<Texture2D>(ConfigValues.FrontFaceTextures);
		Globals.DeckTexture = Resources.Load<Texture2D>(string.Format(ConfigValues.BackFaceTexture, ConfigValues.DeckColor.ToString())); ;

		Load();
	}

	private void Load()
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

	public List<CardModel> GetCards(int quantity = 5)
	{
		var cards = new List<CardModel>();

		try
		{
			if (Cards != null && quantity <= Cards.Count)
			{
				while (DealtCards.Count < quantity)
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
		int index = Random.Next(Cards.Count);
		DealtCards.Add(Cards[index]);
		Cards.RemoveAt(index);
		return DealtCards.Last();
	}

	public CardModel ReplaceCard(CardModel cardToReplace)
	{
		cardToReplace.IsSelected = false;
		Cards.Add(cardToReplace);
		DealtCards.Remove(cardToReplace);
		return GetCard();
	}
}