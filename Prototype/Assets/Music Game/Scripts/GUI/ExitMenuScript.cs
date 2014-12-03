using UnityEngine;
using System.Collections;

public class ExitMenuScript : MonoBehaviour
{
	private bool gameSaved = false;

	void Start()
	{
		StartCoroutine("SaveGame");
	}
	
	void Update()
	{
		if (gameSaved)
		{
			Application.Quit();
		}
	}

	IEnumerator SaveGame()
	{
		GlobalValues.instance.SaveGame();

		gameSaved = true;

		return null;
	}
}
