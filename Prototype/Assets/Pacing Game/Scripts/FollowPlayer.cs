using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		transform.position = player.transform.position;
	}
}
