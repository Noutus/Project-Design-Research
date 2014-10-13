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
		RaycastHit hitFinal = new RaycastHit();

		for (int i = 0; i < maxIterations; i++)
		{
			direction = Vector2Helper.Rotate(direction, 360 / maxIterations);

			RaycastHit[] hit = Physics.RaycastAll(transform.position, direction, 10, 1<<8);
			if(hit.Length>0){
			if (hit[0].collider != null)
			{
				if (i == 0)
				{
					hitFinal = hit[0];
				}

				else 
				{
					if (Vector3.Distance(transform.position, hit[0].point) < Vector3.Distance(transform.position, hitFinal.point))
				    {
						hitFinal = hit[0];
					}
				}
			}
			}
		}

		melody.transform.position = hitFinal.point;
	}
}
