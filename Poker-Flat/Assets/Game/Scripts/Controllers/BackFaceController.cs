using UnityEngine;

public class BackFaceController : MonoBehaviour
{
	private void Awake()
	{
		GlobalResources.OnBackFaceTextureChanged += OnBackFaceTextureChanged;
	}

	private void OnBackFaceTextureChanged(object sender, Texture2D texture)
	{
		GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
	}
}
