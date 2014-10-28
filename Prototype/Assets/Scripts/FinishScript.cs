using UnityEngine;
using System.Collections;

public class FinishScript : MonoBehaviour
{
	GameObject player;

	public int currentLevel;

	void Start()
	{
		player = GameObject.Find("Player");
	}

	void Update()
	{
		if (Vector3.Distance(player.transform.position, transform.position) < 5)
		{
			int level = currentLevel + 1;

			if (GlobalValues.instance.LevelsUnlocked < level)
			{
				GlobalValues.instance.LevelsUnlocked = level;
				GlobalValues.instance.SaveGame();

				Debug.Log("Unlocked level: " + GlobalValues.instance.LevelsUnlocked);
			}

			GameObject go;
			if (go = GameObject.Find("Statistics Line"))
			{
				go.GetComponent<PlayerLineScript>().ExportTexture();
			}

			string name = "Level " + level.ToString();
			if (Application.CanStreamedLevelBeLoaded(name))
			{
				Application.LoadLevel(name);
			}

			else
			{
				Application.LoadLevel("Menu Main");
			}
		}
	}
}
