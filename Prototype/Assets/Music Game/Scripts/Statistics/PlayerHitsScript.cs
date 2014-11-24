using UnityEngine;
using System;
using System.Collections;
using System.IO;

// Author: Arnout Verburg

public class PlayerHitsScript : StatisticsImageScript
{
	private static int hitSize = 5;

	void Start()
	{
		Initialize();
	}
	
	void Update()
	{
		UpdateFull();
	}

	public override void Initialize ()
	{
		filename = "Hits";

		base.Initialize();
	}

	public void Hit(Vector3 position)
	{
		Vector2 hit = WorldToTexturePosition(position);
		Draw((int)hit.x, (int)hit.y);
	}

	private void Draw (int x, int y)
	{
		Color[] filling = new Color[hitSize * hitSize];
		for (int i = 0; i < filling.Length; i++)
			filling[i] = Color.red;
		texture.SetPixels(x, y, hitSize, hitSize, filling);
	}
}
