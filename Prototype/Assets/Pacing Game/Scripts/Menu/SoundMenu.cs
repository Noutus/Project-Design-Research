using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundMenu : MonoBehaviour
{
	private static SoundMenu S_instance;
	public static SoundMenu Instance
	{
		get
		{
			if (S_instance == null)
			{
				S_instance = GameObject.Find("Menu").GetComponent<SoundMenu>();
			}

			return S_instance;
		}
	}

	public AudioClip[] menuSounds;  

	public int[] menuReferences;

	private int index;

	private bool upPressed;
	private bool downPressed;

	private float keyDelay;

	private static float keyDelayMax = 1;

	public GameObject finishLine;

	private bool activeMenu = true;

	void Update()
	{
		if (activeMenu)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1)) MusicGlobals.instance.Level = 0;
		    if (Input.GetKeyDown(KeyCode.Alpha2)) MusicGlobals.instance.Level = 1;
		    if (Input.GetKeyDown(KeyCode.Alpha3)) MusicGlobals.instance.Level = 2;
		    if (Input.GetKeyDown(KeyCode.Alpha4)) MusicGlobals.instance.Level = 3;

			if (Input.GetKeyDown(KeyCode.Return) || Input.GetAxis("Fire1") > 0.2) SelectMenuItem(index);

			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") > 0.2) upPressed = true;
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") < -0.2) downPressed = true;
			if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") < 0.2) upPressed = false;
			if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") > -0.2) downPressed = false;

			if (!audio.isPlaying && keyDelay > keyDelayMax)
			{
				if (upPressed) ChangeMenuItem(index--);
				if (downPressed) ChangeMenuItem(index++);
			}

			keyDelay += Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetAxis("Back") > 0.2)
		{
			if (activeMenu) Application.Quit();
			else ReturnToMenu();
		}
	}

	private void SelectMenuItem(int i)
	{
		if (i < 0) i += menuSounds.Length;
		i = i % menuSounds.Length;

		MenuBackgroundMusic.Instance.ToggleMusic(false);

		//MusicGlobals.instance.Level = menuReferences[i];

		finishLine.GetComponent<PacingFinish>().Initialize();
		
		if (Camera.main != null) Camera.main.transform.position = Vector3.forward * -10;
		activeMenu = false;
	}

	private void ChangeMenuItem(int i)
	{
		while (i < 0) i += menuSounds.Length;
		i = i % menuSounds.Length;

		audio.PlayOneShot(menuSounds[i]);

		keyDelay = 0;
	}

	public void ReturnToMenu()
	{
		PlayerController.Instance.ResetPosition();
		MenuBackgroundMusic.Instance.ToggleMusic(true);

		activeMenu = true;
		if (Camera.main != null) Camera.main.transform.position = Vector3.forward * 40;
		PacingFinish.instance.EndLevel();
	}
}
