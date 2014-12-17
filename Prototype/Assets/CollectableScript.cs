using UnityEngine;
using System.Collections;

public class CollectableScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
				GameObject.FindGameObjectWithTag("MiddleLine").GetComponent<ChaserMovement>().decreaseIndex();
				GameObject.Destroy (gameObject);
		}
	}
}
