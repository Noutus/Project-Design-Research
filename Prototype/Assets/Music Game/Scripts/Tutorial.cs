using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
	private TutorialState currentState;
	public enum TutorialState
	{
		Intro,
		Start,
		Controls,
		Move,
		Return,
		Reverse,
		End,
	}

	private AudioSource[] audioSources;

	private MusicController playerMusicController;
	private Melody playerMelody;
	private Movement playerMovement;

	private bool doneMove;
	private bool doneReturn;
	private bool doneReverse;

	void Start()
	{
		currentState = TutorialState.Intro;
		audioSources = GetComponents<AudioSource>();
		audioSources[0].Play();

		GameObject player = GameObject.Find("Player");
		playerMusicController = player.GetComponent<MusicController>();
		playerMelody = player.GetComponent<Melody>();
		playerMovement = player.GetComponent<Movement>();

		SetPlayer(false);

		doneMove = false;
		doneReturn = false;
		doneReverse = false;
	}
	
	void Update()
	{
		switch(currentState)
		{
		case TutorialState.Intro:
			if (!audioSources[0].isPlaying)
			{
				audioSources[1].Play();
				currentState = TutorialState.Controls;
			}
			break;
		case TutorialState.Start:
			if (!audioSources[2].isPlaying)
			{
				audioSources[3].Play();
				currentState = TutorialState.Move;
				playerMusicController.enabled = true;
				playerMelody.enabled = true;
			}
			break;
		case TutorialState.Controls:
			if (!audioSources[1].isPlaying)
			{
				audioSources[2].Play();
				currentState = TutorialState.Start;
			}
			break;
		case TutorialState.Move:
			if (doneMove || !audioSources[3].isPlaying)
			{
				doneMove = true;
				SetPlayer(true);

				if (!doneReturn && GameObject.Find("WallLoop").GetComponent<MusicSystem>().check)
				{
					audioSources[4].Play();
					currentState = TutorialState.Return;
					MovePlayer();
					SetPlayer(false);
				}

				if (!doneReverse && GameObject.Find("OrchestraReversed").GetComponent<AudioSource>().isPlaying)
				{
					audioSources[5].Play();
					currentState = TutorialState.Reverse;
					Debug.Log(currentState);
					MovePlayer();
					SetPlayer(false);
				}

				if (!doneReverse && playerMovement.transform.position.y > 90)
				{
					playerMovement.transform.rotation = Quaternion.Euler(0, 0, 180);
					playerMovement.transform.position = new Vector2(playerMovement.transform.position.x, 50);
				}

				if (doneReturn && doneReverse)
				{
					audioSources[6].Play();
					currentState = TutorialState.End;
					Debug.Log(currentState);
					MovePlayer();
					SetPlayer(false);
				}
			}
			break;
		case TutorialState.Return:
			doneReturn = true;
			if (!audioSources[4].isPlaying)
			{
				SetPlayer(true);

				if (!GameObject.Find("WallLoop").GetComponent<MusicSystem>().check)
				{
					currentState = TutorialState.Move;
					Debug.Log(currentState);
				}
			}
			break;
		case TutorialState.Reverse:
			doneReverse = true;
			if (!audioSources[5].isPlaying)
			{
				SetPlayer(true);

				if (GameObject.Find("Orchestra").GetComponent<AudioSource>().isPlaying)
				{
					currentState = TutorialState.Move;
					Debug.Log(currentState);
				}
			}
			break;
		case TutorialState.End:
			if (!audioSources[6].isPlaying)
			{
				SetPlayer(true);
			}
			break;
		}
	}

	private void SetPlayer(bool b)
	{
		playerMusicController.enabled = b;
		playerMelody.enabled = b;
		playerMovement.enabled = b;
		if (!b) playerMovement.Reset();
	}

	private void MovePlayer()
	{
		Quaternion q = playerMovement.transform.rotation;
		playerMovement.transform.rotation = Quaternion.identity;
		playerMovement.transform.Translate(Vector2.right * 25);
		playerMovement.transform.rotation = q;
	}
}
