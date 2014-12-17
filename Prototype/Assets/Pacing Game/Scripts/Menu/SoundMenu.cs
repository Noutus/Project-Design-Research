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

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return)) SelectMenuItem(index);

		if (Input.GetKeyDown(KeyCode.UpArrow)) upPressed = true;
		if (Input.GetKeyDown(KeyCode.DownArrow)) downPressed = true;
		if (Input.GetKeyUp(KeyCode.UpArrow)) upPressed = false;
		if (Input.GetKeyUp(KeyCode.DownArrow)) downPressed = false;

		if (!audio.isPlaying && keyDelay > keyDelayMax)
		{
			if (upPressed) ChangeMenuItem(index--);
			if (downPressed) ChangeMenuItem(index++);
		}

		keyDelay += Time.deltaTime;
	}

	private void SelectMenuItem(int i)
	{
		i = i % menuSounds.Length;

		MusicGlobals.instance.Level = menuReferences[i];

		finishLine.GetComponent<PacingFinish>().Initialize();
	}

	private void ChangeMenuItem(int i)
	{
		i = i % menuSounds.Length;

		audio.PlayOneShot(menuSounds[i]);

		keyDelay = 0;
	}
}
