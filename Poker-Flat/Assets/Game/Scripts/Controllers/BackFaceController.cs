using UnityEngine;

public class BackFaceController : MonoBehaviour
{
	private void Awake()
	{
		Globals.OnDeckTextureChanged += OnDeckTextureChanged;
	}

	private void OnDeckTextureChanged(object sender, Texture2D texture)
	{
		GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
	}
}
