using cakeslice;

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Quaternion = UnityEngine.Quaternion;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	private MeshRenderer _frontFaceRenderer;
	[SerializeField]
	private MeshRenderer _backFaceRenderer;

	private CardModel _card;
	private Outline _outline;

	private bool isLerping;

	private void Awake()
	{
		_outline = GetComponent<Outline>();
		_outline.enabled = false;
	}

	private void Start()
	{
		//_outline.enabled = false;
	}

	public void SetValues(CardModel card)
	{
		if (card != null)
		{
			_card = card;
			_frontFaceRenderer.material.SetTexture("_MainTex", _card.FrontTexture);
			_backFaceRenderer.material.SetTexture("_MainTex", _card.DeckTexture);
		}
	}

	public IEnumerator RotateZ(float degrees)
	{
		if (!isLerping)
		{
			isLerping = true;

			var transform = GetComponent<Transform>();
			for (float i = 0; i <= degrees; i++)
			{
				transform.rotation = Quaternion.Euler(0f, 0f, i);
				yield return new WaitForSeconds(0f);
			}
		}

		isLerping = false;
	}

	public IEnumerator MoveTo(Vector3 end, float moveDuration)
	{
		if (!isLerping)
		{
			isLerping = true;
			float t = 0.0f;

			while (t < moveDuration)
			{
				t += Time.deltaTime;
				transform.position = Vector3.Lerp(transform.position, end, t / moveDuration);

				if (Vector3.Distance(transform.position, end) < 0.001f)
				{
					isLerping = false;
					yield break;
				}

				yield return null;
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_outline.enabled = true;
		//var pos = transform.position;

		//if (!isLerping)
		//	StartCoroutine(MoveTo(new Vector3(pos.x, pos.y + .1f, pos.z), .2f));
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_outline.enabled = _card.IsSelected;
		//var pos = transform.position;

		//if (!isLerping)
		//	StartCoroutine(MoveTo(new Vector3(pos.x, pos.y - .1f, pos.z), .2f));
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_card.IsSelected = !_card.IsSelected;
		_outline.enabled = _card.IsSelected;
	}
}
