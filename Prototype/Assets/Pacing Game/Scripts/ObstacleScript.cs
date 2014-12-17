using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MiddlePoint m = GameObject.FindGameObjectWithTag ("MiddleLine").GetComponent<MiddleLine> ().points [2];
		transform.position = m.Position;
		transform.rotation = Quaternion.Euler(0, 0, m.Angle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
				if (col.gameObject.tag == "Player") {
			audio.Play();
			   			while(audio.isPlaying){
						Debug.Log ("hhhh");
				}
				
						if (!Input.GetKey (KeyCode.Space)) {
							GameObject.FindGameObjectWithTag("MiddleLine").GetComponent<ChaserMovement>().increaseIndex();
						}
						GameObject.Destroy (gameObject);
				}
		}
}
