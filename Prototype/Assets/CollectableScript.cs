using UnityEngine;
using System.Collections;

public class CollectableScript : MonoBehaviour {

	public GameObject prefab;
	public GameObject prefab2;

	public AudioClip[] sounds;
	// Use this for initialization
	void Start ()
	{
		/*Question: shall we spawn this object in MiddlePoint position? (as we do with the obstacle)
		 * 
		MiddlePoint m = GameObject.FindGameObjectWithTag ("MiddleLine").GetComponent<MiddleLine> ().points [2]; // I have taken this point just randomly
		transform.position = m.Position;
		transform.rotation = Quaternion.Euler(0, 0, m.Angle);
		
		*/
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (Vector3.Distance(col.transform.position, transform.position) < 2)
			{
				Debug.Log("Collectable Success");

				GameObject.FindGameObjectWithTag("MiddleLine").GetComponent<ChaserMovement>().decreaseIndex();
				GameObject.Instantiate(prefab);
			}

			else
			{
				Debug.Log("Collectable Fail");

				GameObject.Instantiate(prefab2);
			}

			CollectableController.Instance.RemoveCollectable(gameObject);
			Destroy(gameObject);
		}
	}

	public void SetHeight(int i, float time)
	{
		audio.Stop();
		audio.clip = sounds[i];
		audio.time = time;
		audio.Play();
	}
}
