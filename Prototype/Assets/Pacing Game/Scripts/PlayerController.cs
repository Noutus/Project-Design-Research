using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private static PlayerController S_instance;
	public static PlayerController Instance
	{
		get
		{
			if (S_instance == null)
			{
				S_instance = (PlayerController) GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerController));
			}

			return S_instance;
		}
	}

	public float speed;
	private float maxTurn = 90f,horizRotate=0f;
	public float angle, angle2;
	private Vector3 wallDistance;

	public GameObject middleLine;

	private bool upPressed;
	private bool leftPressed;
	private bool rightPressed;

	public bool jumpAllowed = false;
	private float moveCooldown = 0;

	void Start()
	{
		Chaser.Instance.SetNewPlayer(gameObject);
		GameObject.Find("Audio Listener").GetComponent<FollowPlayer>().SetPlayer(gameObject);

		GameObject[] g = GameObject.FindGameObjectsWithTag("Instrument");
		foreach (GameObject go in g)
		{
			MusicPosition m;
			if (m = go.GetComponent<MusicPosition>()) m.player = gameObject;
		}
	}

	void Update()
	{
		if (middleLine != null)
		{

			/*float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			if (moveVertical != 0 && moveHorizontal != 0) {
				rotating(moveHorizontal,moveVertical);
				Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
				rigidbody.AddForce (movement * speed * Time.deltaTime); //we multiply it by DeltaTime to make it frame rate independent
			}
		}   

		/*void rotating(float horizontal,float vertical){
			// Create a new vector of the horizontal and vertical inputs.
			Vector3 targetDirection = new Vector3(horizontal, vertical,0f);
			
			// Create a rotation based on this new vector assuming that up is the global y axis.
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
			
			// Create a rotation that is an increment closer to the target rotation from the player's rotation.
			Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, 3 * Time.deltaTime);
			
			// Change the players rotation to this new rotation.
			rigidbody.MoveRotation(newRotation);

		}*/


			//transform.Translate (new Vector3 (0, 10, 0) * Time.deltaTime); 
			//new Vector3(0,1,0)  == transform.up
			//transform.Translate (new Vector3 (0, 10, 0) * Time.deltaTime);   ===  transform.position += transform.up * Time.deltaTime * 10;


			//this just works for the Y axis
			Vector3 targetDir = transform.position.normalized; 
			Vector3 forward = transform.up.normalized;
			angle = middleLine.GetComponent<MiddleLine> ().AngleBetween (gameObject);
			angle2 = angle;
			if (angle > 180) angle -= 360;
			//Debug.Log ("angle " + angle);
			horizRotate= Mathf.Clamp (angle, -90, 90);
			//Debug.Log (horizRotate);

			//transform.rotation = Quaternion.Euler(0, horizRotate, 0);
	//		Debug.Log (angle);
	//		if (angle > 90f)
	//			print("close");
	//
	//
			//Simulate the wall by limiting the player movement to a given distance with respect to the line

			if (Input.GetKey (KeyCode.W) || Input.GetAxis("Vertical") > 0.2f || Input.GetAxis("BatStrafe") < -0.2f) upPressed = true;
			if (moveCooldown > 0) upPressed = false;
			if (Input.GetKey (KeyCode.A) || Input.GetAxis("Horizontal") < -0.2f) leftPressed = true;
			if (Input.GetKey (KeyCode.D) || Input.GetAxis("Horizontal") > 0.2f) rightPressed = true;

			if (upPressed)
			{	
				transform.position += transform.up * Time.deltaTime * speed;
			}

			if (leftPressed && (maxTurn - horizRotate) > 0)
			{
				transform.Rotate (Vector3.forward, maxTurn * Time.deltaTime);
			}

			if (rightPressed && (maxTurn + horizRotate) > 0)
			{
				transform.Rotate (Vector3.forward, -maxTurn * Time.deltaTime);
			}

			upPressed = leftPressed = rightPressed = false;

			if ( moveCooldown > 0) moveCooldown -= Time.deltaTime;
			if (!jumpAllowed && (Input.GetKey(KeyCode.Space) || Input.GetAxis("Fire1") > 0.2f)) moveCooldown = 0.5f;
		}
	}

	public void SetTrack(GameObject g)
	{
		middleLine = g;
	}
}
