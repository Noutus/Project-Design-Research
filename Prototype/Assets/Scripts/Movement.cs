using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	private bool upPressed;
	private bool downPressed;
	private bool leftPressed;
	private bool rightPressed;

	private float maxSpeed;
	private float maxTurn;

	void Start()
	{
		upPressed = false;
		downPressed = false;
		leftPressed = false;
		rightPressed = false;

		maxSpeed = 10f;
		maxTurn = 100f;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "FinishObject") {
			Application.LoadLevel ("BatGame");
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
			upPressed = true;

		if (Input.GetKeyDown(KeyCode.S))
			downPressed = true;

		if (Input.GetKeyDown(KeyCode.A))
			leftPressed = true;

		if (Input.GetKeyDown(KeyCode.D))
			rightPressed = true;
		
		if (Input.GetKeyUp(KeyCode.W))
			upPressed = false;

		if (Input.GetKeyUp(KeyCode.S))
			downPressed = false;

		if (Input.GetKeyUp(KeyCode.A))
			leftPressed = false;

		if (Input.GetKeyUp(KeyCode.D))
			rightPressed = false;

		if (upPressed)
			transform.position += transform.up * Time.deltaTime * maxSpeed;

		if (downPressed)
			transform.position -= transform.up * Time.deltaTime * maxSpeed;

		if (leftPressed)
			transform.Rotate(Vector3.forward, maxTurn * Time.deltaTime);

		if (rightPressed)
			transform.Rotate(Vector3.forward, -maxTurn * Time.deltaTime);
	}
}
