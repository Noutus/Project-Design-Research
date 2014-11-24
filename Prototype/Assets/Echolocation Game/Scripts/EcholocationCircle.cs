using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EcholocationCircle : MonoBehaviour
{
	private bool pulsing;

	private float echoTime;

	private static float maxEchoTime = 3;

	public GameObject soundPrefab;

	private List<GameObject> collisions;

	void Start()
	{
		pulsing = false;

		echoTime = 0;

		collisions = new List<GameObject>();
	}
	
	void Update()
	{
		// When Space is pressed, the player starts sending out a pulse of sound, which returns an echo.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Starting pulse...");
			pulsing = true;
			echoTime = 0;
		}

		// The actual code for the echo. Only runs when the pulse is activated.
		if (pulsing)
		{
			foreach (GameObject go in collisions)
			{
				Destroy(go);
			}

			collisions.Clear();

			RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, echoTime * 4, Vector3.up, echoTime * 3);

			foreach (RaycastHit2D hit in hits)
			{
				if (hit.transform.tag == "Wall")
				{
					Vector3 hitPosition = new Vector3(hit.point.x, hit.point.y, 0);
					collisions.Add((GameObject) GameObject.Instantiate(soundPrefab, hitPosition, Quaternion.identity));
				}
			}

			// Keep track of the time since the player started the echo.
			echoTime += Time.deltaTime;

			// Stop sending a pulse when it exceeds the time limit.
			if (echoTime >= maxEchoTime)
			{
				Debug.Log("Stopping pulse...");
				pulsing = false;

				foreach (GameObject go in collisions)
				{
					Destroy(go);
				}
				
				collisions.Clear();
			}
		}
	}
}
