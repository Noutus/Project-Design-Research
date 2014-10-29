using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	private bool upPressed;
	private bool downPressed;
	private bool leftPressed;
	private bool rightPressed;
	private bool strafeLeftPressed;
	private bool strafeRightPressed;

	private float maxSpeed;
	private float maxTurn;

	void Start()
	{
		upPressed = false;
		downPressed = false;
		leftPressed = false;
		rightPressed = false;
		strafeLeftPressed = false;
		strafeRightPressed = false;

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
		/*
		if (Input.GetKeyDown(KeyCode.W)) upPressed = true;
		if (Input.GetKeyDown(KeyCode.S)) downPressed = true;
		if (Input.GetKeyDown(KeyCode.A)) leftPressed = true;
		if (Input.GetKeyDown(KeyCode.D)) rightPressed = true;
		if (Input.GetKeyDown(KeyCode.Q)) strafeLeftPressed = true;
		if (Input.GetKeyDown(KeyCode.E)) strafeRightPressed = true;
		
		if (Input.GetKeyUp(KeyCode.W)) upPressed = false;
		if (Input.GetKeyUp(KeyCode.S)) downPressed = false;
		if (Input.GetKeyUp(KeyCode.A)) leftPressed = false;
		if (Input.GetKeyUp(KeyCode.D)) rightPressed = false;
		if (Input.GetKeyUp(KeyCode.Q)) strafeLeftPressed = false;
		if (Input.GetKeyUp(KeyCode.E)) strafeRightPressed = false;
		*/

		if (Input.GetKey(KeyCode.W) || Input.GetAxis("BatForward") > 0) upPressed = true;
		else upPressed = false;

		if (Input.GetKey(KeyCode.A) || Input.GetAxisRaw("BatTurn") < -0.1f)
		{
			leftPressed = true;
			rightPressed = false;
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetAxisRaw("BatTurn") > 0.1f)
		{
			rightPressed = true;
			leftPressed = false;
		}
		else
		{
			leftPressed = false;
			rightPressed = false;
		}

		if (Input.GetKey(KeyCode.E) || Input.GetAxisRaw("BatStrafe") < -0.1f)
		{
			strafeLeftPressed = false;
			strafeRightPressed = true;
		}

		else if (Input.GetKey(KeyCode.Q) || Input.GetAxisRaw("BatStrafe") > 0.1f)
		{
			strafeLeftPressed = true;
			strafeRightPressed = false;
		}

		else
		{
			strafeRightPressed = false;
			strafeLeftPressed = false;
		}

		if (upPressed) transform.position += transform.up * Time.deltaTime * maxSpeed;
		if (downPressed) transform.position -= transform.up * Time.deltaTime * maxSpeed;
		if (leftPressed) transform.Rotate(Vector3.forward, maxTurn * Time.deltaTime);
		if (rightPressed) transform.Rotate(Vector3.forward, -maxTurn * Time.deltaTime);
		if (strafeLeftPressed) transform.position -= transform.right * maxSpeed * Time.deltaTime;
		if (strafeRightPressed) transform.position += transform.right * maxSpeed * Time.deltaTime;
	}

	public void Reset()
	{
		upPressed = false;
		downPressed = false;
		leftPressed = false;
		rightPressed = false;
		strafeLeftPressed = false;
		strafeRightPressed = false;
	}
}
