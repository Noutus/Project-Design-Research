using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/* ATTACHED TO THE PLAYER
 * PURPOSE: this script has two main options:
 1.RAYCASTING: it carries out a raycast and places the collisions (EchoSoundLooped) GameObject in the corresponding places on the walls
 2.STOP/PLAY MELODY: 
  2.1:it recognizes which collision is closest to the player
  2.2:it checks whether the player is closer enough to the wall as to Play/Stop the main melody
 


*/
public class EcholocationCone : MonoBehaviour
{
	public GameObject collisionObject;
	private static int nrOfSounds = 7;
	private static Vector3 hiddenPosition = new Vector3(1000, 0);
	private GameObject[] collisions;
	private GameObject reference,wallLoop;
	private AudioSource[] melody;
	public bool revers;

	void Start()
	{
		//melody = (AudioSource[])GameObject.Find ("Melody(Clone)").GetComponents<AudioSource>();
		melody = new AudioSource[2];
		melody [0] = GameObject.Find ("Orchestra").GetComponent<AudioSource>();
		melody [1] = GameObject.Find ("OrchestraReversed").GetComponent<AudioSource>();


		wallLoop = GameObject.Find("WallLoop");
		collisions = new GameObject[nrOfSounds];
		revers = false;
		for (int i = 0; i < collisions.Length; i++)
		{
			collisions[i] = (GameObject) GameObject.Instantiate(collisionObject);
			collisions[i].transform.position = hiddenPosition;
		}
	}

	void pauseMainMelody(){
		//TODO: here is the problem, the thing is that it takes the reference for every AudioSource (that's why it is able to show 
		//its name in the Debug.Log BUT the sentence to stop it " e.audio.Pause (); " only works with the first one (the piano) 
		//WHY??????????????????????
		foreach (AudioSource e in melody) {
			if(e.isPlaying){

				e.audio.Pause ();
				//Debug.Log(i+"Pauseee: " + e.clip.name + " and is playing?: " + e.audio.isPlaying );

			}

		}
	}
	
	void playMainMelody(){

		//foreach (AudioSource e in melody) {
			//if (!e.isPlaying) {
			if (!revers) {
				if(melody[1].isPlaying)
				melody [1].audio.Stop ();
			  if(!melody[0].isPlaying)
					melody [0].audio.Play ();
			} else {
				if(melody[0].isPlaying)
				melody [0].audio.Stop ();
			   if(!melody[1].isPlaying)

				melody [1].audio.Play ();
			}

		}


	public void reverse(){
		if (melody [0].isPlaying) {
			melody [0].audio.Stop ();

			revers=true;
		} else {
			melody [1].audio.Stop ();

			revers=false;
		}
	}

	void Update()
	{
		
		//1: RAYCASTING

		RaycastHit[] hits = new RaycastHit[nrOfSounds];

		for (int i = 0; i < hits.Length; i++)
		{
			// Calculate the direction of the raycast.
			float rayDistance=1f;
			bool aux=true;
			Vector2 direction = transform.up;
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
						collisions[i].transform.eulerAngles = new Vector3(hits[i].transform.eulerAngles.x,
						                                                  hits[i].transform.eulerAngles.y,
						                                                  hits[i].transform.eulerAngles.z);
					
						aux=false;
						rayDistance=1f;
					}else{
						collisions[i].transform.position=hiddenPosition;
					}	

			
		}

	}

}


		//2: STOP/PLAY MELODY


		//2.1
		Vector3 playerPosition = transform.position;
		float playerWallDistance = 100000f;
		foreach(GameObject e in collisions){
			if(Vector3.Distance(playerPosition,e.transform.position) <= playerWallDistance){
				playerWallDistance=Vector3.Distance(playerPosition,e.transform.position);
				reference=e;
			}
		}

		reference.renderer.material.color = Color.red; //this is just to check which collision is closest to the player


		//calculation of the angles


		//2.2

		if(playerWallDistance < 3f){
			pauseMainMelody();
			wallLoop.GetComponent<MusicSystem>().check=true;
			wallLoop.GetComponent<MusicSystem>().collision=reference;
		}else{
			wallLoop.GetComponent<MusicSystem>().check=false;
			playMainMelody();
		}
}
}