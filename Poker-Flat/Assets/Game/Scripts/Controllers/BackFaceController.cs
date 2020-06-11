using UnityEngine;

public class BackFaceController : MonoBehaviour
{
	private void OnEnable()
	{
		Globals.OnDeckTextureChanged += OnDeckTextureChanged;

	}
	private void OnDisable()
	{
		Globals.OnDeckTextureChanged -= OnDeckTextureChanged;
	}

	private void Start()
	{
		GetComponent<MeshRenderer>().material.SetTexture("_MainTex", Globals.DeckTexture);
	}

	private void OnDeckTextureChanged(object sender, Texture2D texture)
	{
		GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
	}
}
