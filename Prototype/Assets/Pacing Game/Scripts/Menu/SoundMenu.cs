using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundMenu : MonoBehaviour
{
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
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetAxis("Fire1") > 0.2) SelectMenuItem(index);

			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0.2) upPressed = true;
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < -0.2) downPressed = true;
			if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Vertical") < 0.2) upPressed = false;
			if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetAxis("Vertical") > -0.2) downPressed = false;

			if (!audio.isPlaying && keyDelay > keyDelayMax)
			{
				if (upPressed) ChangeMenuItem(index--);
				if (downPressed) ChangeMenuItem(index++);
			}

			keyDelay += Time.deltaTime;
		}
	}

	private void SelectMenuItem(int i)
	{
		i = i % menuSounds.Length;

		MusicGlobals.instance.Level = menuReferences[i];

		finishLine.GetComponent<PacingFinish>().Initialize();

		activeMenu = false;
	}

	private void ChangeMenuItem(int i)
	{
		if (i < 0) i += menuSounds.Length;
		i = i % menuSounds.Length;

		audio.PlayOneShot(menuSounds[i]);

		keyDelay = 0;
	}
}
