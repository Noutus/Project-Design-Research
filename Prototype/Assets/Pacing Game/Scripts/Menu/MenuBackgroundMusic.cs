using UnityEngine;
using System.Collections;

public class MenuBackgroundMusic : MonoBehaviour
{
	private static MenuBackgroundMusic S_instance;
	public static MenuBackgroundMusic Instance
	{
		get
		{
			if (S_instance == null)
			{
				S_instance = GameObject.Find("Menu Music").GetComponent<MenuBackgroundMusic>();
			}

			return S_instance;
		}
	}

	public void ToggleMusic(bool b)
	{
		if (!b) audio.Stop();
		else audio.Play();
	}
}
