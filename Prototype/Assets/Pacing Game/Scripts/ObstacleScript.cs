using UnityEngine;
using System.Collections;

/*
 * This object represents the obstacle once the player has collided with the ObstacleStart
 * It starts playing the first song, then when the first song finishes it check whether the player has pressed the button to avoid the obstacle
 * and plays a good or bad music as a result.
 * One question could be: shall we make this object to be in the same position that the player so that they can listen to it?
 * 
 */
public class ObstacleScript : MonoBehaviour
{
	private GameObject player;

	public AudioClip startSong,goodResult,badResult;

	private bool pressed = false;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		PlayerController.Instance.jumpAllowed = true;

		audio.playOnAwake = false;
		audio.clip = startSong;
		audio.Play();

		Invoke ("jumpingPlayer",audio.clip.length);
	}

	void Update()
	{
		transform.position = player.transform.position;

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Fire1") > 0.2f) pressed = true;
	}

	void jumpingPlayer()
	{
		PlayerController.Instance.jumpAllowed = false;

		//then we decide which music to play
		if (!pressed)
		{
			Debug.Log("Obstacle Fail");
			audio.clip = badResult;
			audio.Play();
			GameObject.FindGameObjectWithTag ("MiddleLine").GetComponent<ChaserMovement>().increaseIndex ();
		}

		else
		{
			Debug.Log("Obstacle Success");
			audio.clip = goodResult;
			audio.Play();

		}

		GameObject.Destroy (gameObject,audio.clip.length);
	}
}
