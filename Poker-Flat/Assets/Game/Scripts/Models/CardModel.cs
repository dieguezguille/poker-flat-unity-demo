﻿using UnityEngine;

public class CardModel
{
	public Texture2D FrontTexture { get; set; }
	public Texture2D DeckTexture { get; set; }
	public CardController Controller { get; set; }
	public GameObject GameObject { get; set; }
	public CardColor Color { get; set; }
	public CardSuit Suit { get; set; }
	public int Value { get; set; }
	public bool IsSelected { get; set; }
}
