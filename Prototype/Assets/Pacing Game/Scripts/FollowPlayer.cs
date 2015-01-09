using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		if (player != null)
		{
			transform.position = player.transform.position;
			transform.rotation = player.transform.rotation;
		}

		else
		{
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
		}
	}

	public void SetPlayer(GameObject g)
	{
		player = g;
	}
}
