using UnityEngine;
using System.Collections;

public class PixcelateEffect : MonoBehaviour {

	public bool on = false;

	[Range(1, 200)]
	public int horizontal = 20;

	[Range(1, 200)]
	public int vertical = 20;

	public Material material;

	private bool flag;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if(on)
		{
			Graphics.Blit (src, dest, material);
		}
		else
		{
			Graphics.Blit (src, dest);
		}
	}

	void Update()
	{
		if(on)
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				flag = !flag;
			}

			if (flag)
			{
				material.SetInt("_Horizontal", horizontal);
				material.SetInt("_Vertical", vertical);
			}
			else
			{
				material.SetInt("_Horizontal", 5000);
				material.SetInt("_Vertical", 5000);
			}
		}
	}
}