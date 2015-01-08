using UnityEngine;
using System.Collections;

/* This Prefab represents the start of an obstacle
 * It simply identify when the player has collided with it and then it creates an ObstacleObject instance.
 */
public class ObstacleStart : MonoBehaviour
{
	public GameObject prefab;

/*
	void Start()
	{
		MiddlePoint m = GameObject.FindGameObjectWithTag("MiddleLine").GetComponent<MiddleLine>().points[2]; // I have taken this point just randomly
		transform.position = m.Position;
		transform.rotation = Quaternion.Euler(0, 0, m.Angle);
	}
*/

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			ObstacleController.Instance.AddObstacle(prefab);
			Destroy(gameObject);
		}
	}
}
