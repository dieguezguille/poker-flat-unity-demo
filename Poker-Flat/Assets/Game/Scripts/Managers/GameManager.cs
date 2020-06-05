using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Start()
	{
		Init();
	}

	private void Init()
	{
		var loaded = CardManager.Instance.LoadCards();
		var cards = CardManager.Instance.DealCards();
	}
}
