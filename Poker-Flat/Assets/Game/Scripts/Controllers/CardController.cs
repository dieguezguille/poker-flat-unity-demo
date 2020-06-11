using cakeslice;
using DG.Tweening;

using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[HideInInspector]
	public CardModel Model;

	[HideInInspector]
	public Outline _outline;
	[HideInInspector]
	public BoxCollider _collider;
	[HideInInspector]
	public Vector3 _initialPos;

	[SerializeField]
	private MeshRenderer _frontFaceRenderer;
	[SerializeField]
	private MeshRenderer _backFaceRenderer;

	private AudioSource _audioSource;

	private void Awake()
	{
		_outline = GetComponent<Outline>();
		_collider = GetComponent<BoxCollider>();
		_audioSource = GetComponent<AudioSource>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Highlight(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Highlight(false);
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		ToggleSelection();
	}

	public void SetValues(CardModel model)
	{
		if (model != null)
		{
			Model = model;
			_frontFaceRenderer.material.SetTexture("_MainTex", Model.FrontTexture);
			_backFaceRenderer.material.SetTexture("_MainTex", Model.DeckTexture);
		}
	}

	public void Replace()
	{
		var card = DeckManager.Instance.ReplaceCard(Model);

		Sequence seq = DOTween.Sequence();
		seq.Append(gameObject.transform.DOMove(new Vector3(_initialPos.x, _initialPos.y + .1f, _initialPos.z), .2f));
		seq.Append(gameObject.transform.DORotate(new Vector3(0, 0, 180), .5f).SetEase(Ease.InOutBack));
		seq.Append(gameObject.transform.DOMove(DeckManager.Instance.Deck.position, .7f).OnComplete(() =>
		{
			SetValues(card);
			_outline.enabled = false;
		}));
		seq.AppendInterval(.1f);
		seq.Append(gameObject.transform.DOMove(new Vector3(_initialPos.x, _initialPos.y + .1f, _initialPos.z), .7f));
		seq.Append(gameObject.transform.DORotate(new Vector3(0, 0, 0), .5f).SetEase(Ease.InOutBack));
		seq.Append(gameObject.transform.DOMove(_initialPos, .2f));
	}

	public void MoveTo(Vector3 position, float duration = .2f)
	{
		gameObject.transform.DOMove(position, duration);
	}

	public void RotateTo(Vector3 finalRotation, float duration = .2f)
	{
		gameObject.transform.DORotate(finalRotation, duration);
	}

	private void Highlight(bool highlighted)
	{
		if (Vector3.Distance(gameObject.transform.position, _initialPos) < 0.1)
		{
			_outline.enabled = highlighted || Model.IsSelected;

			if (highlighted)
			{
				MoveUp();
			}
			else
			{
				MoveDown();
			}
		}
	}

	private void ToggleSelection()
	{
		var selectedCards = DeckManager.Instance.DealtCards.Where(x => x.IsSelected).ToList();

		Model.IsSelected = !Model.IsSelected && selectedCards.Count < 3;
		_outline.enabled = Model.IsSelected;

		if (Model.IsSelected)
		{
			MoveUp();
		}
		else
		{
			MoveDown();
		}
	}

	private void MoveUp()
	{
		_audioSource.Play();
		gameObject.transform.DOMove(new Vector3(_initialPos.x, _initialPos.y + .1f, _initialPos.z), .2f);
	}

	private void MoveDown()
	{
		gameObject.transform.DOMove(_initialPos, .2f);
	}

	//private void OnHiglightEvent(object sender, bool highlighted)
	//{
	//	if (!_isTweening)
	//	{
	//		var pos = transform.position;

	//		if (highlighted)
	//		{
	//			gameObject.transform.DOMove(new Vector3(pos.x, _maxY, pos.z), .1f);
	//		}
	//		else
	//		{
	//			gameObject.transform.DOMove(new Vector3(pos.x, _minY, pos.z), .1f);
	//		}
	//	}
	//}

	//public IEnumerator MoveTo(Vector3 end, float moveDuration, Action function = null)
	//{
	//	float t = 0.0f;

	//	while (t < moveDuration)
	//	{
	//		t += Time.deltaTime;
	//		transform.position = Vector3.Lerp(transform.position, end, t / moveDuration);

	//		if (Vector3.Distance(transform.position, end) < 0.001f)
	//		{
	//			transform.position = end;
	//			function?.Invoke();
	//			yield break;
	//		}

	//		yield return null;
	//	}
	//}

	//public IEnumerator RotateTo(Vector3 end, float rotateDuration, Action function = null)
	//{

	//	float t = 0.0f;

	//	while (t < rotateDuration)
	//	{
	//		t += Time.deltaTime;
	//		var rot = transform.rotation;
	//		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(end), t / rotateDuration);

	//		if (Vector3.Distance(transform.rotation.eulerAngles, Quaternion.Euler(end).eulerAngles) < 0.01f)
	//		{
	//			transform.rotation = Quaternion.Euler(end);
	//			function?.Invoke();
	//			yield break;
	//		}

	//		yield return null;
	//	}
	//}
}