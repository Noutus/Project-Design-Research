using UnityEngine;
using System.Collections;

//the Radar GameObject represent the two collisions points which go up  
public class Radar : MonoBehaviour {

	
	public GameObject SoundPrefab;
	public int maxNumberPoints;
	private GameObject[maxNumberPoints] SoundVector;


	// Use this for initialization
	void Start () {
		maxNumberPoints = 6;
		SoundVector=(GameObject) GameObject.Instantiate(SoundPrefab);

	}
	
	// Update is called once per frame
	void Update () {
		//this sentence make the two collision points(radar GameObject) go up in the Y axis
		transform.position = transform.position + Vector3.up * 0.05f;


		RaycastHit2D[] vector = Physics2D.CircleCastAll (transform.position, 4f, Vector3.up);

		if (vector.Length != 0) {
			 foreach(RaycastHit2D e in vector){
				Debug.Log ( " Hit in position: " + e.point);
				}
	}
}
}