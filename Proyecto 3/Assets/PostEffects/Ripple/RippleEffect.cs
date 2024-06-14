using UnityEngine;
using System.Collections;

public class RippleEffect : MonoBehaviour {

	public bool on = false;

	[Range(0, 0.05f)]
	public float strength = 0.01f;

	[Range(1, 50)]
	public int amount = 10;

	[Range(0f, 20f)]
	public float speed = 10f;

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
				material.SetFloat("_Strength", strength);
				material.SetInt("_Amount", amount);
				material.SetFloat("_Speed", speed);
			}
			else
			{
				material.SetFloat("_Strength", 0);
			}
		}
	}
}