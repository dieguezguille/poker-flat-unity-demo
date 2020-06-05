using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Start()
	{
		Init();
	}

	private void Init()
	{
		CardManager.Instance.LoadCards();
		CardManager.Instance.DealCards();
	}
}
