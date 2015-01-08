using UnityEngine;
using System.Collections;

public class BadSystem : MonoBehaviour
{
	public AudioClip music;

	public void PlayMusic(float musicTime)
	{
		audio.clip = music;
		audio.time = musicTime;
		audio.Play();
	}
}
