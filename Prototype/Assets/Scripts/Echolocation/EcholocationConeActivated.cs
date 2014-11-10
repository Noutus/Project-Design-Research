using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EcholocationConeActivated : MonoBehaviour
{
	private bool pulsing;

	private static int nrOfSounds = 7;

	private static float maxEchoTime = 3;

	private float echoTime;

	private static Vector3 hiddenPosition = new Vector3(1000, 0);

	public GameObject soundPrefab;

	private GameObject[] collisions;

	void Start()
	{
		pulsing = false;

		echoTime = 0;

		collisions = new GameObject[nrOfSounds];
		
		for (int i = 0; i < collisions.Length; i++)
		{
			collisions[i] = (GameObject) GameObject.Instantiate(soundPrefab);
			collisions[i].transform.position = hiddenPosition;
		}
	}
	
	void Update()
	{
		// When Space is pressed, the player starts sending out a pulse of sound, which returns an echo.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			pulsing = true;

			echoTime = 0;
		}

		// The actual code for the echo. Only runs when the pulse is activated.
		if (pulsing)
		{
			UpdateSoundPositions();
		}
	}

	private void UpdateSoundPositions()
	{
		RaycastHit[] hits = new RaycastHit[nrOfSounds];
		
		for (int i = 0; i < hits.Length; i++)
		{
			Vector3 direction = transform.up;
			direction = Vector2Helper.Rotate(direction, 10 * (i - (nrOfSounds - 1) / 2));
			
			RaycastHit[] firstHits = Physics.RaycastAll(transform.position, direction);
			
			for (int j = 0; j < firstHits.Length; j++)
			{
				if (firstHits[j].transform.tag == "Wall")
				{
					hits[i] = firstHits[j];
					break;
				}
			}
			
			Vector3 hitPosition = new Vector3(hits[i].point.x, hits[i].point.y, 0);
			
			if (hitPosition == Vector3.zero) hitPosition = hiddenPosition;
			
			collisions[i].transform.position = hitPosition;
		}
		
		// Keep track of the time since the player started the echo.
		echoTime += Time.deltaTime;
		
		// Stop sending a pulse when it exceeds the time limit.
		if (echoTime >= maxEchoTime)
		{
			Debug.Log("Stopping pulse...");
			pulsing = false;
			
			for (int i = 0; i < collisions.Length; i++)
			{
				collisions[i].transform.position = hiddenPosition;
			}
		}
	}
}
