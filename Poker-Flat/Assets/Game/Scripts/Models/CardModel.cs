using UnityEngine;

public class CardModel
{
	public Texture2D FrontFaceTexture { get; set; }
	public Texture2D BackFaceTexture { get; set; }
	public CardColor Color { get; set; }
	public CardSuit Suit { get; set; }
	public int Value { get; set; }
}
