using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBShiftEffect : MonoBehaviour {

	public bool on = false;

	[Range(0f, 0.1f)]
	public float amount = 0.05f;

	[Range(0f, 10f)]
	public float speed = 5f;

	public Material material;

	private bool flag;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if(on)
		{
			Graphics.Blit(src, dest, material);
		}
		else
		{
			Graphics.Blit(src, dest);
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
				material.SetFloat("_Amount", amount);
				material.SetFloat("_Speed", speed);
			}
			else
			{
				material.SetFloat("_Amount", 0);
				material.SetFloat("_Speed", 0);
			}
			
		}
	}
}