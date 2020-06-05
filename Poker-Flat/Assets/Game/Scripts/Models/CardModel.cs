using UnityEngine;

public class CardModel
{
	public Texture2D FrontFaceTexture { get; set; }
	public Texture2D BackFaceTexture
	{
		get
		{
			return Resources.Load<Texture2D>($"Sprites/Cards/Back/Blue");
		}
	}

	public CardColor Color { get; set; }
	public CardSuit Suit { get; set; }
	public int Value { get; set; }
}
