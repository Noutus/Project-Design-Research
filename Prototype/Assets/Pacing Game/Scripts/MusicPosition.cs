using UnityEngine;
using System.Collections;

public class MusicPosition : MonoBehaviour
{
	public GameObject player;
	public MiddleLine middleLine;

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
			transform.position = middleLine.GetPointsIndex(middleLine.ClosestIndex(player) + 4).Position;
		}
	}
}
