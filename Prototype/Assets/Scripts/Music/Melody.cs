using UnityEngine;
using System.Collections;

public class Melody : MonoBehaviour
{
	public GameObject melodyPrefab;

	private GameObject melody;

	private static int maxIterations = 32;

	void Start()
	{
		melody = (GameObject) GameObject.Instantiate(melodyPrefab);
	}
	
	void Update()
	{
		Vector2 direction = Vector2.up;
		RaycastHit2D hitFinal = new RaycastHit2D();

		for (int i = 0; i < maxIterations; i++)
		{
			direction = Vector2Helper.Rotate(direction, 360 / maxIterations);

			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10, 1<<8);

			if (hit.collider != null)
			{
				if (i == 0)
				{
					hitFinal = hit;
				}

				else 
				{
					if (Vector3.Distance(transform.position, hit.point) < Vector3.Distance(transform.position, hitFinal.point))
				    {
						hitFinal = hit;
					}
				}
			}
		}

		melody.transform.position = hitFinal.point;
	}
}
