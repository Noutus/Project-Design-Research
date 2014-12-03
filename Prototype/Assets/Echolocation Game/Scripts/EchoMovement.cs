using UnityEngine;
using System.Collections;

public class EchoMovement : MonoBehaviour
{
	private bool leftPressed;
	private bool rightPressed;

	private float maxTurn;

	void Start()
	{
		leftPressed = false;
		rightPressed = false;

		maxTurn = 100f;
	}

	void Update()
	{
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

		if (leftPressed) transform.Rotate(Vector3.forward, maxTurn * Time.deltaTime);
		if (rightPressed) transform.Rotate(Vector3.forward, -maxTurn * Time.deltaTime);
	}

	public void Reset()
	{
		leftPressed = false;
		rightPressed = false;
	}
}
