using UnityEngine;
using System.Collections;

public class LevelMenuScript : MainMenuScript
{
	void Start()
	{
		Initialize();

		int unlocks = (int) GlobalValues.instance.LevelsUnlocked;

		for (int i = 0; i < MenuItems.Length; i++)
		{
			if (i < unlocks)
			{
				MenuItems[i].locked = false;
			}

			else
			{
				MenuItems[i].locked = true;
			}
		}
	}
}
