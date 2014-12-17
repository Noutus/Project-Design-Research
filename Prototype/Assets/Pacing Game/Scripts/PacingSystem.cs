using UnityEngine;
using System.Collections;

public class PacingSystem : MonoBehaviour {

	// Use this for initialization
	private GameObject []instruments;
	public float angle;

	void Start () {
	  //use findObjectsW
		instruments=(GameObject[])GameObject.FindGameObjectsWithTag("Instrument");
	}
	
	// Update is called once per frame
	void Update () {
		angle = GetComponent<PlayerController> ().angle2;

		if (angle > 180) angle = 360 - angle;
		foreach (GameObject e in instruments) {
			AudioSource music=e.GetComponent<AudioSource>();
			float parameter=e.GetComponent<MusicParameters>().parameter;
			//first we check the "good" instruments
			if(e.GetComponent<MusicParameters>().isGood){
				if(angle < 45){
					if(angle > parameter){
						music.volume = (45 + parameter - angle)/45;
//						Debug.Log("MUSIC" + music.volume);
					}else{
						music.volume=1;
					}
				}else{
					music.volume=0;
				}

			//else we check the "bad" instruments
			}else{
				if(angle > 45){
					if(angle > parameter){
						music.volume = 90-(90-angle)/90;
						}else{
						music.volume=0;
							}
					}else{
						music.volume=0;
					}
			}	
	}
	}
}
