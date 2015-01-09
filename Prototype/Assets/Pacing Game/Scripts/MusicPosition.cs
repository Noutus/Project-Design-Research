using UnityEngine;
using System.Collections;

public class MusicPosition : MonoBehaviour
{
	public GameObject player;
	public MiddleLine middleLine;

	private Vector3 from;
	private Vector3 to;
	private Vector3 previousTo;

	private float lerpTime;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if (GameObject.FindGameObjectWithTag("MiddleLine"))
		{
			middleLine = GameObject.FindGameObjectWithTag("MiddleLine").GetComponent<MiddleLine>();
		}
	}

	void Update()
	{
		if (player != null && middleLine != null)
		{
			if (lerpTime < 1)
			{
				lerpTime += Time.deltaTime * 5;
			}

			to = middleLine.GetPointsIndex(middleLine.ClosestIndex(player) + 4).Position;

			if (to != previousTo)
			{
				from = previousTo;
				lerpTime = 0;
			}

			transform.position = Vector3.Lerp(from, to, lerpTime);

			previousTo = to;
		}
	}
}
