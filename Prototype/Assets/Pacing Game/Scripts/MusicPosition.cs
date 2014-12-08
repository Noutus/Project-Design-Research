using UnityEngine;
using System.Collections;

public class MusicPosition : MonoBehaviour
{
	public GameObject player;
	public MiddleLine middleLine;

	void Update()
	{
		middleLine = GameObject.Find("MiddleLine").GetComponent<MiddleLine>();
		transform.position = middleLine.GetPointsIndex(middleLine.ClosestIndex(player) + 4).Position;
	}
}
