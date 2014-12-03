using UnityEngine;
using System.Collections;

public class EchoWalkMovement : MonoBehaviour
{
	private bool upPressed;
	private bool leftPressed;
	private bool rightPressed;
	private bool strafeLeftPressed;
	private bool strafeRightPressed;

	private float maxSpeed;
	private float maxTurn;

	void Start()
	{
		upPressed = false;
		leftPressed = false;
		rightPressed = false;
		strafeLeftPressed = false;
		strafeRightPressed = false;

		maxSpeed = 10f;
		maxTurn = 100f;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("BatForward") > 0) upPressed = true;
		else upPressed = false;

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("BatTurn") < -0.1f)
		{
			leftPressed = true;
			rightPressed = false;
		}

		else if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow)  || Input.GetAxisRaw("BatTurn") > 0.1f)
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

		if (strafeRightPressed)
		{
			transform.position += transform.up * Time.deltaTime * maxSpeed;
		}

		if (leftPressed)
		{
			transform.Rotate(Vector3.forward, maxTurn * Time.deltaTime);
		}

		if (rightPressed)
		{
			transform.Rotate(Vector3.forward, -maxTurn * Time.deltaTime);
		}
	}

	public void Reset()
	{
		upPressed = false;
		leftPressed = false;
		rightPressed = false;
		strafeLeftPressed = false;
		strafeRightPressed = false;
	}
}
