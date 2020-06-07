using cakeslice;
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

	private void Awake()
	{
		_outline = GetComponent<Outline>();
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
		var transform = GetComponent<Transform>();
		for (float i = 0; i <= degrees; i++)
		{
			transform.rotation = Quaternion.Euler(0f, 0f, i);
			yield return new WaitForSeconds(0f);
		}
	}

	public IEnumerator MoveTo(Vector3 end, float moveDuration)
	{
		float t = 0.0f;

		while (t < moveDuration)
		{
			t += Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, end, t / moveDuration);

			if (Vector3.Distance(transform.position, end) < 0.001f)
			{
				transform.position = end;
				yield break;
			}

			yield return null;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_outline.enabled = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_outline.enabled = _card.IsSelected;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_card.IsSelected = !_card.IsSelected;
		_outline.enabled = _card.IsSelected;
	}
}
