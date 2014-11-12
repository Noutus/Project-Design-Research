using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class EcholocationSound : MonoBehaviour
{
	private SoundState currentState;
	public enum SoundState
	{
		Start,
		Flying,
		Idle,
	}

	private Vector3 moveDirection;

	public static float moveSpeed = 5;

	private AudioSource sound;

	void Awake()
	{
		currentState = SoundState.Start;
	}

	void Start()
	{
		sound = GetComponent<AudioSource>();
	}
	
	void Update()
	{
		if (currentState == SoundState.Flying)
		{
			transform.position += moveDirection * moveSpeed * Time.deltaTime;
		}

		if (currentState == SoundState.Idle)
		{
			if (!sound.isPlaying)
			{
				Destroy(gameObject);
				Destroy(this);
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Wall" && currentState == SoundState.Flying)
		{
			currentState = SoundState.Idle;
			sound.Play();
		}
	}

	public void SetDirection(Vector3 direction)
	{
		moveDirection = direction;
		currentState = SoundState.Flying;
	}
}
