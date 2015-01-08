using UnityEngine;
using System.Collections;

public class PacingSystem : MonoBehaviour
{
	// Use this for initialization
	private GameObject[] instruments;
	public GameObject[] Instruments
	{
		get { return instruments; }
		set { instruments = value; }
	}

	public float angle;

	void Start()
	{
		instruments = MusicTracker.instance.ActiveSystem;
		Debug.Log(instruments.Length);
	}

	void Update()
	{
		PlayMusic();
	}

	private void PlayMusic()
	{
		if (instruments != null && instruments.Length > 0)
		{
			angle = GetComponent<PlayerController>().angle2;
			
			if (angle > 180) angle = 360 - angle;
			
			foreach (GameObject e in instruments)
			{
				AudioSource music=e.GetComponent<AudioSource>();
				float parameter=e.GetComponent<MusicParameters>().parameter;
				float parameter2 = e.GetComponent<MusicParameters>().parameter2;

				//first we check the "good" instruments
				if(e.GetComponent<MusicParameters>().isGood)
				{
					if (angle < parameter) music.volume = 0.5f;
					else if (angle > parameter2) music.volume = 0;
					else music.volume = 0.5f - (0.5f / (parameter2 - parameter) * (angle - parameter));
				}
				
				//else we check the "bad" instruments
				else
				{
					if (angle < parameter) music.volume = 0;
					else if (angle > parameter2) music.volume = 0.5f;
					else music.volume = 0.5f / (parameter2 - parameter) * (angle - parameter);
				}	
			}
		}
	}
}
