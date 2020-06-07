using System;
using UnityEngine;

public static class Globals
{
	public static event EventHandler<Texture2D> OnDeckTextureChanged;

	private static Texture2D deckTexture;
	public static Texture2D DeckTexture
	{
		get
		{
			return deckTexture;
		}
		set
		{
			deckTexture = value;
			OnDeckTextureChanged?.Invoke(null, deckTexture);
		}
	}

	public static Texture2D[] CardTextures { get; set; }
	public static GameObject CardPrefab { get; set; }
}
