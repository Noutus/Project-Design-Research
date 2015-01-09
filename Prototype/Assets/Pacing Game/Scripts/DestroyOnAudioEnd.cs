using UnityEngine;
using System.Collections;

public class DestroyOnAudioEnd : MonoBehaviour
{
	private float elapsedTime;

	void Awake()
	{
		elapsedTime = 0;
	}

	void Update()
	{
		if (elapsedTime > audio.clip.length)
		{
			Destroy(gameObject);
		}

		elapsedTime += Time.deltaTime;
	}
}
