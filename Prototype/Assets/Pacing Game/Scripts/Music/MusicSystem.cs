using UnityEngine;
using System.Collections;

public class MusicSystem : MonoBehaviour
{
	public AudioClip intro;
	public AudioClip[] phases;
	public AudioClip lastPhase;
	public AudioClip transition;

	public GameObject badMusicPrefab;
	public GameObject badMusic;

	void Awake()
	{
		badMusic = GameObject.Instantiate(badMusicPrefab) as GameObject;
	}

	public void StopMusic()
	{
		audio.Stop();
		badMusic.audio.Stop();
	}

	public void PlayMusic(MusicTracker.MusicState state)
	{
		PlayMusic(state, 0, 0);
	}

	public void PlayMusic(MusicTracker.MusicState state, int phaseIndex, float musicTime)
	{
		audio.Stop();
		badMusic.audio.Stop();

		switch (state)
		{
		case MusicTracker.MusicState.Intro:
			audio.clip = intro;
			break;
		case MusicTracker.MusicState.Phase:
			audio.clip = phases[phaseIndex];
			badMusic.GetComponent<BadSystem>().PlayMusic(musicTime);
			break;
		case MusicTracker.MusicState.LastPhase:
			audio.clip = lastPhase;
			badMusic.GetComponent<BadSystem>().PlayMusic(musicTime);
			break;
		case MusicTracker.MusicState.Transition:
			audio.clip = transition;
			badMusic.audio.Stop();
			break;
		}
		
		audio.time = musicTime;
		audio.Play();

		CollectableController.Instance.SetCollectableTimings(musicTime);
	}
}
