using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EcholocationCone : MonoBehaviour
{
	public GameObject soundPrefab;

	private static int nrOfSounds = 7;

	private static Vector3 hiddenPosition = new Vector3(1000, 0);

	private GameObject[] collisions;
	private GameObject reference,lastreference,wallLoop;
	float playerWallDistance;
	private bool firstTime;
	private AudioSource[] melody;
	void Start()
	{
		melody = (AudioSource[])GameObject.Find ("Melody(Clone)").GetComponents<AudioSource>();
		firstTime = true;
		lastreference = null;
		wallLoop = GameObject.Find ("WallLoop");
		// Create sound objects.
		collisions = new GameObject[nrOfSounds];
		for (int i = 0; i < collisions.Length; i++)
		{
			collisions[i] = (GameObject) GameObject.Instantiate(soundPrefab);
			collisions[i].transform.position = hiddenPosition;
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "FinishObject") {
			Application.LoadLevel ("BatGame");
		}
	}

	IEnumerator MyMethod() {

		yield return new WaitForSeconds(10);

	}


	void pauseMainMelody(){
		//TODO: here is the problem, the thing is that it takes the reference for every AudioSource (that's why it is able to show 
		//its name in the Debug.Log BUT the sentence to stop it " e.audio.Pause (); " only works with the first one (the piano) 
		//WHY??????????????????????
		foreach (AudioSource e in melody) {
			if(e.isPlaying){
				e.audio.Pause ();
				//Debug.Log("Pauseee: " + e.clip.name);
			}
		}
	}
	
	void playMainMelody(){
		foreach (AudioSource e in melody) {
			if(!e.isPlaying){
				e.audio.Play ();
				//Debug.Log("Playy: " + e.clip.name);
			}
		}
	}







	void Update()
	{
		RaycastHit[] hits = new RaycastHit[nrOfSounds];

		for (int i = 0; i < hits.Length; i++)
		{
			// Calculate the direction of the raycast.
			float rayDistance=1f;
			bool aux=true;
			Vector2 direction = transform.up;
			//direction = Vector2Helper.Rotate(direction, 10 * (i - (nrOfSounds - 1) / 2));
			direction = Vector2Helper.Rotate(direction, 360 / hits.Length * i);

			// Raycast all the way and only return the walls as a hit.
			while(rayDistance<100f && aux){

				RaycastHit[] firstHits = Physics.RaycastAll(transform.position, direction,rayDistance);
				rayDistance=rayDistance+1;
			
			for (int j = 0; j < firstHits.Length && aux==true; j++)
			{
				if (firstHits[j].transform.tag == "Wall")
				{
						hits[i] = firstHits[j];
						Vector3 hitPosition = new Vector3(hits[i].point.x, hits[i].point.y);
						collisions[i].transform.position = hitPosition;
						aux=false;
						rayDistance=1f;
					}else{
						collisions[i].transform.position=hiddenPosition;
					}	

			
		}

	}

}
		Vector3 ko = transform.position;
		playerWallDistance = 100000f;
		//playerWallDistance = Vector3.Distance(ko,collisions[0].transform.position);
		foreach(GameObject e in collisions){

			if(Vector3.Distance(ko,e.transform.position) <= playerWallDistance){
				playerWallDistance=Vector3.Distance(ko,e.transform.position);
				//Debug.Log("changinnggg :" + e.GetHashCode());
				reference=e; //TODO: this causes problems: this reference is not always the same

			}
		}
		reference.renderer.material.color = Color.red;
		//Debug.Log("this is the referenceee " + reference.GetHashCode());

		if (firstTime) {
			firstTime = false;
			lastreference = reference;
		}

		//Debug.Log ("minimum distance " +playerWallDistance);
		if(playerWallDistance < 3f){

			pauseMainMelody();
			if (reference != lastreference && !firstTime) {
				//Debug.Log ("isnottt");
				lastreference.GetComponent<MusicSystem>().check=false;
			}
			lastreference=reference;
			wallLoop.GetComponent<MusicSystem>().collision=reference;
			wallLoop.GetComponent<MusicSystem>().check=true;

		}else{
			//Debug.Log("weeeeeeeeeeeeeeeeeeeeeeeeeeeee");
			//lastreference.GetComponent<MusicSystem>().check=false;
			//reference.GetComponent<MusicSystem>().check=false;
			wallLoop.GetComponent<MusicSystem>().check=false;
			playMainMelody();
		}
}
}