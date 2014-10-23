using UnityEngine;
using System;
using System.Collections;
using System.IO;

// Author: Arnout Verburg

public class StatisticsImageScript : MonoBehaviour
{
	// Scale between the World and Image.
	protected static int scale = 7;

	// Texture on which the statistics are saved.
	public Texture2D texture;

	// Delay for applying the Texture2D. Using Texture2D.Apply() every frame causes framerate issues.
	private float applyDelay;

	// Name for the file. Always declared in the child scripts.
	protected string filename;

	// Initialize is called in the Start() function of child scripts. Use this for initializing standards.
	public virtual void Initialize()
	{
		// Texture2D is created.
		texture = new Texture2D(100 * scale, 100 * scale);

		// 
		StartCoroutine("PrintLevel");
	}

	public virtual void UpdateFull()
	{
		if (applyDelay > 5)
		{
			UpdateTexture();
			applyDelay = 0;
		}
		
		applyDelay += Time.deltaTime;
		
		if (Input.GetKeyDown(KeyCode.Return))
		{
			ExportTexture();
		}
	}

	protected void UpdateTexture()
	{
		texture.Apply();
	}

	protected void ExportTexture()
	{
		// Convert Texture2D to .png.
		byte[] image = texture.EncodeToPNG();

		// Create the string for the filename.
		string dateNow = DateTime.Now.ToString();
		dateNow = dateNow.Replace(@"/","-");
		dateNow = dateNow.Replace(@":","-");

		// Write image to a .png file.
		File.WriteAllBytes(Application.dataPath + "/../Prints/" + filename + " " + dateNow + ".png", image);
	}

	IEnumerator PrintLevel()
	{
		for (int x = 0; x < texture.width; x++)
		{
			for (int y = 0; y < texture.height; y++)
			{
				Color c;
				RaycastHit hit;
				if (Physics.Raycast(new Vector3((x + 0.5f) / scale, (y + 0.5f) / scale, -10), Vector3.forward,out hit))
				{
					if (hit.collider && hit.collider.transform.name != "Player")
						c = new Color(0, 0, 0, 1);
					else
						c = new Color(1, 1, 1, 0);
					if (texture.GetPixel(x, y).r < 1)
						texture.SetPixel(x, y, c);
				}
			}
			yield return null;
		}
	}
	
	protected Vector2 WorldToTexturePosition(Vector3 worldPosition)
	{
		Vector2 texturePosition = new Vector2(Mathf.Round(worldPosition.x * scale), Mathf.Round(worldPosition.y * scale));
		return texturePosition;
	}
}
