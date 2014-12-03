using UnityEngine;
using System;
using System.Collections;

public class EchoShapeController : MonoBehaviour
{
	private EchoAngleState currentState;
	public enum EchoAngleState
	{
		Score,
		Playing,
	}

	public GameObject[] shapePrefabs;

	private ShapeStatistics stats;
	private GameObject player;
	private GameObject wall;
	private GameObject camera;

	private string currentShape;
	private string chosenShape;

	private Rect leftRect;
	private Rect rightRect;
	private Rect inputRect;

	public GUIStyle leftStyle;
	public GUIStyle rightStyle;
	private GUIStyle inputStyle;

	void Awake()
	{
		currentState = EchoAngleState.Playing;

		leftRect = new Rect(0, 0, Screen.width / 2 - 20, Screen.height);
		rightRect = new Rect(Screen.width / 2 + 20, 0, Screen.width / 2 - 20, Screen.height);
		inputRect = new Rect(Screen.width / 2 - 50, 200, 100, 50);

		inputStyle = new GUIStyle();
		inputStyle.fontSize = 32;
		//inputStyle.font.material.color = Color.white;
	}

	void Start()
	{
		stats = GameObject.Find("Statistics").GetComponent<ShapeStatistics>();

		player = GameObject.Find("Player");
		wall = GameObject.Find("Wall");
		camera = GameObject.Find("Camera");

		RandomPlayerRotation();
		SpawnNewLevel();
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
				SpawnNewLevel();
			}
		}

		if (currentState == EchoAngleState.Playing)
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetAxis("EchoAngleSelect") > 0)
			{
				currentShape = wall.GetComponent<Shape>().ShapeName;

				stats.playMode = "Turn";
				stats.currentShape = currentShape;
				stats.chosenShape = chosenShape;
				stats.ExportValues();

				currentState = EchoAngleState.Score;
			}
		}
	}

	private void RandomPlayerRotation()
	{
		player.transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
	}

	private void SpawnNewLevel()
	{
		chosenShape = "";

		if (wall != null) Destroy(wall);
		wall = GameObject.Instantiate(shapePrefabs[UnityEngine.Random.Range(0, shapePrefabs.Length)]) as GameObject;
	}

	/*void OnGUI()
	{
		if (currentState == EchoAngleState.Playing)
		{
			chosenShape = GUI.TextField(inputRect, chosenShape);

			GUI.TextField(leftRect,
			              "Goal" + Environment.NewLine
			              + Environment.NewLine
			              + "You are in a room with a," + Environment.NewLine
			              + "specific shape. Figure out" + Environment.NewLine
			              + "what shape it is by turning" + Environment.NewLine
			              + "and using your sonic powers."
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
			GUI.TextArea(leftRect,
			             Environment.NewLine
			             + Environment.NewLine
			             + "To play again press" + Environment.NewLine
			             + "the A-Button."
			             , leftStyle);
			if (currentShape == chosenShape)
			{
				GUI.TextArea(rightRect,
				             "Good job!" + Environment.NewLine
				             + Environment.NewLine
				             + "You were inside a " + Environment.NewLine
				             + currentShape
				             , rightStyle);
			}
			
			else
			{
				GUI.TextArea(rightRect,
				             "Try again!" + Environment.NewLine
				             + Environment.NewLine
				             + "You were inside a " + Environment.NewLine
				             + currentShape
				             , rightStyle);
			}
		}
	}*/
}
