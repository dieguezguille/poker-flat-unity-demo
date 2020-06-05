using UnityEngine;

public class CardModel
{
	public CardColor Color { get; set; }
	public CardSuit Suit { get; set; }
	public int Value { get; set; }

	public Texture2D GetFrontFaceTexture()
	{
		return Resources.Load<Texture2D>($"Sprites/Cards/Front/{Color}-{Suit}-{Value}");
	}
	public Texture2D GetBackFaceTexture()
	{
		return Resources.Load<Texture2D>($"Sprites/Cards/Back/Blue");
	}
}
