using UnityEngine;
using System.Collections;

/*ATTACHED TO THE "WALLLOOP" GAMEOBJECT
  PURPOSE:
 *  1. SELECT LOOP: it selects the corresponding loop sounds according to the time in which the main music it is
 *  2. PLAY/STOP LOOP: it plays/stops the loop sound
 * */



public class MusicSystem : MonoBehaviour {

	public bool check;
	private float helper;
	private AudioSource[] audios;
	private bool isInPause;
	private AudioSource looper;
	private AudioSource mainMelody;
	private float aux=1f;

	public GameObject collision;


	void Start () {
		audios=(AudioSource[])GetComponents<AudioSource>();
		isInPause = false;
		check = false;
		helper = 0f;
		looper = audios [1];
		mainMelody = (AudioSource)GameObject.Find ("Orchestra").GetComponent<AudioSource>();
	}

	void Update () {

		//Before performing any unnecesary calculations we check whether the player is closer enough

		if (check) {

			transform.position = collision.transform.position; //go to the closest position
<<<<<<< HEAD
			aux = mainMelody.time;
			//Debug.Log("The main melody is on the time :" + aux+ " secs" );

			// 1. SELECT LOOP
=======
			
			aux = melody[0].time;
			//Debug.Log("The main melody is on the time :" + aux+ " secs" );
			
		   
			
>>>>>>> cd80e9e9307cb247fb479bbe80d547b0170938e4
			helper = 0f;
			if (aux < 28.8f)
				helper = aux;
			else
				helper += Time.deltaTime % 28.8f;

			isInPause = ((aux >= 14.4f && aux < 18.0f) || (aux >= 46.8f && aux < 50.4f)) ? true : false;


			if (isInPause) {
				looper = audios [0].audio;
				helper = 0;
			} else {
				if ((helper >= 0 && helper < 1.8) || (helper >= 7.2 && helper < 9)|| (helper >= 18 && helper < 19.8)
				    || (helper >= 25.2 && helper < 27.0)  || (helper >= 32.4 && helper < 34.2) || (helper >= 39.6 && helper < 41.4)
				    || (helper >= 50.4 && helper < 52.2) || (helper >= 57.6 && helper < 59.4) || (helper >= 64.8 && helper < 66.6)
				    || (helper >= 72.0 && helper < 73.8) || (helper >= 79.2 && helper < 81.0) || (helper >= 86.4 && helper < 88.2)){

					looper = audios [0];

				}
				else if ((helper >= 1.8 && helper < 3.6) || (helper >= 9 && helper < 10.8) || (helper >= 19.8 && helper < 21.6)
				         || (helper >= 27.0 && helper < 28.8) || (helper >= 34.2 && helper < 36.0) || (helper >= 41.4 && helper < 43.2) 
				         || (helper >= 52.2 && helper < 54.0) || (helper >= 59.4 && helper < 61.2) || (helper >= 66.6 && helper < 68.4)
				         || (helper >= 73.8 && helper < 75.6) || (helper >= 81.0 && helper < 82.8) || (helper >= 88.2 && helper < 90.0))
					looper = audios [1];
				else if ((helper >= 3.6 && helper < 5.4) || (helper >= 10.8 && helper < 12.6) || (helper >= 21.6 && helper < 23.4)
				         || (helper >= 28.8 && helper < 30.6) || (helper >= 36.0 && helper < 37.8) || (helper >= 43.2 && helper < 45.0) 
				         || (helper >= 54.0 && helper < 55.8) || (helper >= 61.2 && helper < 63.0) || (helper >= 68.4 && helper < 70.2)
				         || (helper >= 75.6 && helper < 77.4) || (helper >= 82.8 && helper < 84.6) || (helper >= 90.0 && helper < 91.8)){
					looper = audios [2];

					}
				else if ((helper >= 5.4 && helper < 6.3) || (helper >= 12.6 && helper < 13.5) || (helper >= 23.4 && helper < 24.3)
				         || (helper >= 30.6 && helper < 31.5) || (helper >= 37.8 && helper < 38.7) || (helper >= 45.0 && helper < 45.9) 
				         || (helper >= 55.8 && helper < 56.7) || (helper >= 63.0 && helper < 63.9) || (helper >= 70.2 && helper < 71.1)
				         || (helper >= 77.4 && helper < 78.3) || (helper >= 84.6 && helper < 85.5) || (helper >= 91.8 && helper < 92.7))
					looper = audios [3];
				else if ((helper >= 6.3 && helper < 7.2) || (helper >= 24.3 && helper < 25.2) || (helper >= 38.7 && helper < 39.6)
				         || (helper >= 56.7 && helper < 57.6) || (helper >= 71.1 && helper < 72.0) || (helper >= 85.5 && helper < 86.4))
					looper = audios [4];
				else if ((helper >= 13.5 && helper < 14.4) || (helper >= 31.5 && helper < 32.4)  || (helper >= 45.9 && helper < 46.8)
				         || (helper >= 63.9 && helper < 64.8)  || (helper >= 78.3 && helper < 79.2) || (helper >= 92.7 && helper < 93.6))
					//special case for 5.1
					looper = audios [5];
			}

			if(!looper.isPlaying){
					looper.Play ();
			}
<<<<<<< HEAD
			//Debug.Log("this is the actual loop: " + looper.clip.name);

=======
			//}
			//Debug.Log("this is the actual loop: " + looper.clip.name);
>>>>>>> cd80e9e9307cb247fb479bbe80d547b0170938e4

		} else {
			//2. PLAY/STOP LOOP
			if(looper.isPlaying){
				looper.Pause ();
			}
		}
}

}