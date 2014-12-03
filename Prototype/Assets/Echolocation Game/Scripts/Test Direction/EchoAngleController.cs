using UnityEngine;
using System;
using System.Collections;

public class EchoAngleController : MonoBehaviour
{
	private EchoAngleState currentState;
	public enum EchoAngleState
	{
		Score,
		Playing,
	}

	private DirectionStatistics stats;
	private GameObject player;
	private GameObject wall;
	private GameObject camera;

	private float lastScore;

	private Rect leftRect;
	private Rect rightRect;

	public GUIStyle leftStyle;
	public GUIStyle rightStyle;

	void Awake()
	{
		currentState = EchoAngleState.Playing;

		leftRect = new Rect(0, 0, Screen.width / 2 - 20, Screen.height);
		rightRect = new Rect(Screen.width / 2 + 20, 0, Screen.width / 2 - 20, Screen.height);
	}

	void Start()
	{
		stats = GameObject.Find("Statistics").GetComponent<DirectionStatistics>();
		player = GameObject.Find("Player");
		wall = GameObject.Find("Wall");
		camera = GameObject.Find("Camera");

		RandomPlayerRotation();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape) || Input.GetAxisRaw("Back") > 0)
		{
			Application.LoadLevel("Menu Test");
		}

		if (currentState == EchoAngleState.Score)
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("BatForward") > 0)
			{
				currentState = EchoAngleState.Playing;
				RandomPlayerRotation();
			}
		}

		if (currentState == EchoAngleState.Playing)
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetAxis("EchoAngleSelect") > 0)
			{
				Vector2 playRot = Vector2Helper.AngleToVector2(player.transform.rotation.eulerAngles.z);
				Vector2 wallRot = Vector2Helper.AngleToVector2(wall.transform.rotation.eulerAngles.z);

				lastScore = Vector3.Angle(playRot, wallRot);

				stats.deltaAngle = lastScore;
				stats.ExportValues();

				currentState = EchoAngleState.Score;
			}
		}
	}

	private void RandomPlayerRotation()
	{
		player.transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
	}

	void OnGUI()
	{
		if (currentState == EchoAngleState.Playing)
		{
			GUI.TextField(leftRect,
			              "Goal" + Environment.NewLine
			              + Environment.NewLine
			              + "You are in front of a wall," + Environment.NewLine
			              + "try to face the wall with an" + Environment.NewLine
			              + "exact angle of 90 degrees" + Environment.NewLine
			              + "using your sonic powers."
			              , leftStyle);
			GUI.TextField(rightRect,
			              "Controls" + Environment.NewLine
			              + Environment.NewLine
			              + "Left control stick - Turn left and right" + Environment.NewLine
			              + "A-Button - Send sonic pulse" + Environment.NewLine
			              + "B-Button - Confirm your choice."
			              , rightStyle);
		}

		if (currentState == EchoAngleState.Score)
		{
			GUI.TextField(leftRect,
			              Environment.NewLine
			              + Environment.NewLine
			              + "To play again press" + Environment.NewLine
			              + "the B-Button."
			              , leftStyle);
			if (lastScore > 40)
			{
				GUI.TextField(rightRect,
				              "Try again!" + Environment.NewLine
				              + Environment.NewLine
				              + "You were off by " + Environment.NewLine
				              + lastScore.ToString("G3") + " degrees."
				              , rightStyle);
			}

			else
			{
				GUI.TextField(rightRect,
				              "Good job!" + Environment.NewLine
				              + Environment.NewLine
				              + "You were off by only" + Environment.NewLine
				              + lastScore.ToString("G3") + " degrees!"
				              , rightStyle);
			}
		}
	}
}
