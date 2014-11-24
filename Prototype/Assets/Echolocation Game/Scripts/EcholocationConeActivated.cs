using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EcholocationConeActivated : MonoBehaviour
{
	private bool pulsing;

	private static int nrOfSounds = 15;

	private static float maxEchoTime = 0.5f;

	private float echoTime;

	private static Vector3 hiddenPosition = new Vector3(1000, 0);

	public GameObject soundPrefab;

	private GameObject[] collisions;

	private List<GameObject> soundObjects;

	void Start()
	{
		pulsing = false;

		soundObjects = new List<GameObject>();
	}
	
	void Update()
	{
		if (!pulsing && (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("BatForward") > 0))
		{
			pulsing = true;
			echoTime = 0;

			ShootSoundObjects();
		}

		echoTime += Time.deltaTime;

		if (echoTime > maxEchoTime)
		{
			pulsing = false;
			echoTime = 0;
		}
	}

	private void ShootSoundObjects()
	{
		foreach (GameObject go in soundObjects)
		{
			if (go != null) Destroy(go);
		}

		for (int i = 0; i < nrOfSounds; i++)
		{
			Vector3 direction = transform.up;
			direction = Vector2Helper.Rotate(direction, (150 / nrOfSounds) * (i - (nrOfSounds - 1) / 2));

			GameObject sound = GameObject.Instantiate(soundPrefab, transform.position, Quaternion.identity) as GameObject;
			soundObjects.Add(sound);
	
			sound.GetComponent<EcholocationSound>().SetDirection(direction);
		}
	}
}
