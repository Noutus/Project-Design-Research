using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EcholocationCone : MonoBehaviour
{
	public GameObject soundPrefab;

	private static int nrOfSounds = 7;

	private static Vector3 hiddenPosition = new Vector3(1000, 0);

	private GameObject[] collisions;

	void Start()
	{
		// Create sound objects.
		collisions = new GameObject[nrOfSounds];
		for (int i = 0; i < collisions.Length; i++)
		{
			collisions[i] = (GameObject) GameObject.Instantiate(soundPrefab);
			collisions[i].transform.position = hiddenPosition;
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
			direction = Vector2Helper.Rotate(direction, 10 * (i - (nrOfSounds - 1) / 2));

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
					
					

			// Set position of the sound to the raycasthit position.
			
		}
	}
}
}
}