using UnityEngine;
using System.Collections;

public class MusicSystem : MonoBehaviour {

	public bool check;
	private float helper;
	private AudioSource[] audios;
	private bool isInPause;
	private AudioSource looper,looperhelper;
	private AudioSource[] melody;
	private float aux=1f,sampling;
	// Use this for initialization

	public GameObject collision;

	void Start () {
		audios=(AudioSource[])GetComponents<AudioSource>();
		isInPause = false;
		check = false;
		helper = 0f;
		looper = audios [1];
		looperhelper = audios [3].audio;
		//melody = (AudioSource[])GameObject.Find ("Melody(Clone)").GetComponents<AudioSource>();
		//sampling=1/melody[0].clip.frequency;
	}

	void pauseMainMelody(){
		foreach (AudioSource e in melody) {
			if(e.isPlaying){
				e.audio.Pause ();
				Debug.Log("Pauseee: " + e.clip.name);
			}
		}
	}

	void playMainMelody(){
		foreach (AudioSource e in melody) {
			if(!e.isPlaying){
				e.audio.Play ();
				Debug.Log("Playy: " + e.clip.name);
			}
		}
	}
	
	// Update is called once per frame

	IEnumerator MyMethod() {
		
		yield return new WaitForSeconds(5);
		
	}


	void Update () {





		//Before performing any unnecesary calculations we check whether the player is closer enough

		if (check) {

			//pauseMainMelody();

			//Debug.Log("checked");

			transform.position = collision.transform.position; //go to the closest position
			
			//aux = melody [0].timeSamples;
			aux=aux+0.01f;
			
			//Debug.Log ("timeeeeeeeeee" +aux);
			
			helper = 0f;
			if (aux < 28.8f)
				helper = aux;
			else
				helper += Time.deltaTime % 28.8f;




			isInPause = ((aux >= 28.8f && aux <= 32.4f) || (aux >= 90f && aux <= 93.6f)) ? true : false;


			if (isInPause) {
				Debug.Log ("kkkkkkkkkkkk");
				looper = audios [0].audio;
				helper = 0;
			} else {
				if ((helper >= 0 && helper <= 3.6) || (helper >= 14.9 && helper <= 18.5)){
					looper = audios [0];
				//Debug.Log("num1");
				}
				else if ((helper >= 3.7 && helper <= 7.4) || (helper >= 18.6 && helper <= 22.2)){
					looper = audios [1];
					//Debug.Log("num2");
					}
				else if ((helper >= 7.5 && helper <= 11) || (helper >= 25.9 && helper <= 28))
					looper = audios [2];
				else if ((helper >= 11.1 && helper <= 12.9) || (helper >= 21.9 && helper <= 25.5))
					looper = audios [3];
				else if ((helper >= 13.0 && helper <= 14.8) || (helper >= 3.7 && helper <= 3.6))//special case for 5.1
					looper = audios [4];
			}
			//if(looper != looperhelper){
				//Debug.Log("AWAKEE1");
			//}else{
				//Debug.Log("AWAKEE2");
				//looperhelper.Stop();
				if(!looper.isPlaying){
					looper.Play ();
					looperhelper=looper;//TODO: when do we change looperhelper?
				}
			//}
			//Debug.Log("this is the actual loop: " + looper.clip.name);

		} else {
			//Debug.Log("stooooop");
			if(looper.isPlaying){
				//looperhelper.Stop();
				looper.Pause ();
			}


			//playMainMelody();
		}
}

}