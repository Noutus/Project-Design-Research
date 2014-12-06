using UnityEngine;
using System.Collections;

public class PacingMovement : MonoBehaviour
{
	private static float moveSpeed = 4;

	private bool upPressed;
	private bool downPressed;
	private bool leftPressed;
	private bool rightPressed;

	void Start()
	{
		upPressed = false;
		downPressed = false;
		leftPressed = false;
		rightPressed = false;
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
			transform.position += Vector3.up * Time.deltaTime * moveSpeed;
		
		if (downPressed)
			transform.position += Vector3.down * Time.deltaTime * moveSpeed;
		
		if (leftPressed)
			transform.position += Vector3.left * Time.deltaTime * moveSpeed;
		
		if (rightPressed)
			transform.position += Vector3.right * Time.deltaTime * moveSpeed;

		float a = GameObject.Find("MiddleLine").GetComponent<MiddleLine>().AngleBetween(this.gameObject);
		Debug.Log(a);
	}
}
