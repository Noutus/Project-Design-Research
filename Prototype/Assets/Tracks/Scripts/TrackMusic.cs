using UnityEngine;
using System.Collections;

public class TrackMusic : MonoBehaviour
{
	private GameObject player;

	private TrackAngleController angleController;

	private AudioSource audioSource;

	public Vector3 moveDirection;

	/* In stead of Time.deltaTime, use this value for progressing in the game.
	 * Time in Tracks is not dependent on real time, but the speed at which the music plays. */
	private float timeSinceLastFrame;
	public float DeltaMusicTime
	{
		get { return timeSinceLastFrame; }
	}

	private float timeInPreviousFrame;

	void Start()
	{
		player = GameObject.Find("Player");

		angleController = GameObject.Find("Angle Controller").GetComponent<TrackAngleController>();

		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		timeSinceLastFrame = audioSource.audio.time - timeInPreviousFrame;

		Move();
		SetMusicSpeed();

		timeInPreviousFrame = audioSource.audio.time;
	}

	private void Move()
	{
		transform.rotation = Quaternion.Euler(0, 0, angleController.trackAngle);

		moveDirection = Vector2Helper.AngleToVector2(angleController.trackAngle);

		transform.position += moveDirection * DeltaMusicTime;
	}

	private void SetMusicSpeed()
	{
		float a = transform.rotation.eulerAngles.z;
		float b = player.transform.rotation.eulerAngles.z;

		float c = Mathf.DeltaAngle(a, b);

		if (c < 0) c = -c;

		audioSource.audio.pitch = 1 - c / 90;
	}
}