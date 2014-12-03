using UnityEngine;
using System;
using System.Collections;
using System.IO;

// Author: Arnout Verburg

public class PlayerLineScript : StatisticsImageScript
{
	private Transform player;

	void Start()
	{
		Initialize();
	}

	void Update()
	{
		UpdateFull();
	}

	public override void Initialize()
	{
		filename = "Line";

		player = GameObject.Find("Player").transform;

		base.Initialize();
	}

	public override void UpdateFull()
	{
		PrintPlayer();

		base.UpdateFull();
	}

	private void PrintPlayer()
	{
		Vector2 pixelPosition = WorldToTexturePosition(player.position);
		int x = (int) pixelPosition.x;
		int y = (int) pixelPosition.y;
		texture.SetPixel(x, y, new Color(1, 0, 0, 1));
	}
}
