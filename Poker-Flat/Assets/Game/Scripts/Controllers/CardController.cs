using UnityEngine;

public class CardController : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer _frontFaceRenderer;
	[SerializeField]
	private MeshRenderer _backFaceRenderer;

	private CardColor _cardColor;
	private CardSuit _cardSuit;

	private int _cardValue;

	public void SetValues(CardModel card)
	{
		if (card != null)
		{
			_frontFaceRenderer.material.SetTexture("_MainTex", card.FrontFaceTexture);
			_backFaceRenderer.material.SetTexture("_MainTex", card.BackFaceTexture);
			_cardColor = card.Color;
			_cardSuit = card.Suit;
			_cardValue = card.Value;
		}
	}
}
