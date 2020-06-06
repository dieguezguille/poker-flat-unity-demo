using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "", order = 1)]
public class Card : ScriptableObject
{
	public Texture2D FrontFaceTexture;
	public Texture2D BackFaceTexture;
	public CardColor Color;
	public CardSuit Suit;
	public int Value;
}
