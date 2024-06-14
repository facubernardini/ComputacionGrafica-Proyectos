using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicesEffect : MonoBehaviour {

	public bool on = false;

	//[Range(1, 200)]
	//public int slices = 1;

	//[Range(0f, 1f)]
	private float offset;

	[Range(0, 1)]
	public int vertical = 0;

	[Range(0, 1)]
	public int loop = 0;

	public Material material;

	private int slices;
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
				slices = Random.Range(1, 200);
				offset = Random.Range(0.05f, 0.2f);
				material.SetInt("_Slices", slices);
				material.SetFloat("_Offset", offset);
				material.SetInt("_Vertical", vertical);
				material.SetInt("_Loop", loop);
			}
			else
			{
				material.SetInt("_Slices", 1);
				material.SetFloat("_Offset", 0f);
				material.SetInt("_Vertical", vertical);
				material.SetInt("_Loop", loop);
			}
		}
	}
}