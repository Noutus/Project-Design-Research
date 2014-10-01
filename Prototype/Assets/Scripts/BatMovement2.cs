using UnityEngine;
using System.Collections;

public class BatMovement2 : MonoBehaviour {

	private bool upPressed;
	private bool downPressed;
	private bool leftPressed;
	private bool rightPressed;

	public GameObject radar;

	// Use this for initialization
	void Start () {


		upPressed = false;
		downPressed = false;
		leftPressed = false;
		rightPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
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
			transform.position += Vector3.up * Time.deltaTime * 4;
		
		if (downPressed)
			transform.position += Vector3.down * Time.deltaTime * 4;
		
		if (leftPressed)
			transform.position += Vector3.left * Time.deltaTime * 4;
		
		if (rightPressed)
			transform.position += Vector3.right * Time.deltaTime * 4;

		if (Input.GetKeyDown(KeyCode.Space))
		{

			//radar.transform.position = this.transform.position;

			Debug.log(radar.GetComponent<Radar>().numberOfPossiblePoints);
		}
	}
}
