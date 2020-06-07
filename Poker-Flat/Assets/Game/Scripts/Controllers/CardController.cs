﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using Quaternion = UnityEngine.Quaternion;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
			_frontFaceRenderer.material.SetTexture("_MainTex", card.FrontTexture);
			_backFaceRenderer.material.SetTexture("_MainTex", card.DeckTexture);
			_cardColor = card.Color;
			_cardSuit = card.Suit;
			_cardValue = card.Value;
		}
	}

	public IEnumerator RotateZ(float degrees)
	{
		var transform = GetComponent<Transform>();
		for (float i = 0; i <= degrees; i++)
		{
			transform.rotation = Quaternion.Euler(0f, 0f, i);
			yield return new WaitForSeconds(0f);
		}
	}

	public IEnumerator MoveTo(Vector3 position)
	{
		float elapsedTime = 0;
		float waitTime = 2f;

		var rigidBody = GetComponent<Rigidbody>();
		var interpolater = 0.0f;

		while (elapsedTime < waitTime)
		{
			rigidBody.MovePosition(Vector3.Lerp(rigidBody.position, position, interpolater)); 
			interpolater += 0.02f * Time.deltaTime;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		// highlight card
		Debug.Log("entering");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		// disable card highlight
		Debug.Log("leaving");
	}
}
