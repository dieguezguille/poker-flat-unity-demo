using System;
using UnityEngine;

public static class GlobalResources
{
	public static event EventHandler<Texture2D> OnBackFaceTextureChanged;

	private static Texture2D backfaceTexture;
	public static Texture2D BackFaceTexture
	{
		get
		{
			return backfaceTexture;
		}
		set
		{
			backfaceTexture = value;
			OnBackFaceTextureChanged?.Invoke(null, backfaceTexture);
		}
	}
}
