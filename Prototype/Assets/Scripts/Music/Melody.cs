using UnityEngine;
using System.Collections;

public class Melody : MonoBehaviour
{
	public GameObject melodyPrefab;

	private GameObject melody;

	private static int maxIterations = 32;




	void Awake()
	{
		melody = (GameObject) GameObject.Instantiate(melodyPrefab); 
	}



	void Update()
	{

		//first let's check whether the player it is actually over the line
		//in that case we place the melody in its position 


		Vector2 direction = Vector2.up;
		RaycastHit hitFinal = new RaycastHit();
		bool isInLine = false;
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
						/*if(Vector3.Distance(transform.position, hitFinal.point)<0.9f){

							isInLine=true;
						}*/
						    
				}

				else 
				{
					if (Vector3.Distance(transform.position, hit[0].point) < Vector3.Distance(transform.position, hitFinal.point))
				    {
						hitFinal = hit[0];
					}
						/*if(Vector3.Distance(transform.position, hitFinal.point)<0.9f){

							isInLine=true;
						}*/
				}
			}
			}
		}
		// (isInLine) {
		//	melody.transform.position = transform.position;
		//} else {
			melody.transform.position = hitFinal.point;
		//}





	}
}
