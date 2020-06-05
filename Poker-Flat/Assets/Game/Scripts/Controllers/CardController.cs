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

	public void SetValues(CardModel model)
	{
		if (model != null)
		{
			_frontFaceRenderer.material.SetTexture("_MainTex", model.GetFrontFaceTexture());
			_backFaceRenderer.material.SetTexture("_MainTex", model.GetBackFaceTexture());
			_cardColor = model.Color;
			_cardSuit = model.Suit;
			_cardValue = model.Value;
		}
	}
}
