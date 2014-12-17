using UnityEngine;
using System.Collections;

public class ChaserMovement : MonoBehaviour {

	private int index;
	public GameObject player;
	public MiddlePoint[] points;
	public int timeDelayed;
	// Use this for initialization
	void Start () {
		index = 0;
		points = GetComponent<MiddleLine> ().points;
		StartCoroutine (MyMethod ());
	}
	
	// Update is called once per frame
	public void increaseIndex(){
		int i= index + 3;
		for(; index < i ;index++)
			points[index].Counted=true;
	}
	public void decreaseIndex(){
		int i= index - 3;
		for(; index > i ;index--)
			points[index].Counted=true;

	}
	IEnumerator MyMethod() {
		yield return new WaitForSeconds(timeDelayed);
		Debug.Log ("hey");
		points [index].Counted=true;
		MiddlePoint point = GetComponent<MiddleLine> ().ClosestPoint (player);
		int i = 0;
		foreach(MiddlePoint e in points){
			if(e == point){
				break;
			}else{
				i++;
			}
		}
		if (point.Counted) {
			Debug.Log("THE PLAYER HAS LOST");
		}
		else if((i-3)>0 && points[i - 3].Counted){
			Debug.Log ("THE CHASER OBJECT IS CLOSE");
		}
		index++;

		StartCoroutine (MyMethod ());
	}

}
