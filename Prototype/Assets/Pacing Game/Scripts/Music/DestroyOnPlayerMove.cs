using UnityEngine;
using System.Collections;

public class DestroyOnPlayerMove : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKey (KeyCode.W) || Input.GetAxis("Vertical") > 0.2f || Input.GetAxis("BatStrafe") < -0.2f) Destroy (gameObject);
	}
}
