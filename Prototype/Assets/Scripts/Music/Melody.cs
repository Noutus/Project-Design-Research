using UnityEngine;
using System.Collections;


/* ATTACHED TO THE PLAYER
 * PURPOSE: 
 *  1. RAYCASTING: it raycast and detects which is the closest point in the main line to place the Melody
 *  2. PLACING: it places the Melody in the proper position on the line
 * 
 */
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
		//1. RAYCASTING

		Vector2 direction = Vector2.up;
		RaycastHit hitFinal = new RaycastHit();
		bool isInLine = false;
		for (int i = 0; i < maxIterations; i++) {
						direction = Vector2Helper.Rotate (direction, 360 / maxIterations);

						RaycastHit[] hit = Physics.RaycastAll (transform.position, direction, 100, 1 << 8);
						if (hit.Length > 0) {
								if (hit [0].collider != null) {
										if (i == 0) {
												hitFinal = hit [0];
										
												if (Vector3.Distance (transform.position, hitFinal.point) < 0.9f) {
														isInLine = true;
												}
						    
						
										} else {
												if (Vector3.Distance (transform.position, hit [0].point) < Vector3.Distance (transform.position, hitFinal.point)) {
														hitFinal = hit [0];
												}
										}
										if (Vector3.Distance (transform.position, hitFinal.point) < 0.9f) {
												isInLine = true;
										}
								}
						}
				}

		//2. PLACING
		
		//first let's check whether the player it is actually over the line
		//in that case we place the melody in its position

		if(isInLine) {
			melody.transform.position = transform.position;
		} else {
			melody.transform.position = hitFinal.point;
			}
		//Vector2 playRot = new Vector2 (Mathf.Sin(transform.rotation.z * Mathf.Deg2Rad), Mathf.Cos(transform.rotation.z * Mathf.Deg2Rad));
		//Vector2 lineRot = new Vector2 (Mathf.Sin(hitFinal.transform.rotation.z * Mathf.Deg2Rad), Mathf.Cos(hitFinal.transform.rotation.z * Mathf.Deg2Rad));

		Vector2 playRot = Vector2Helper.AngleToVector2(transform.rotation.eulerAngles.z);
		Vector2 lineRot = Vector2Helper.AngleToVector2(hitFinal.transform.rotation.eulerAngles.z);

		Debug.Log("Angle between " + playRot.ToString("G2") + " and " + lineRot.ToString("G2") + " is " + Vector2.Angle(playRot, lineRot));

		if (Vector2.Angle (playRot, lineRot) > 90) {
			GetComponent<EcholocationCone> ().revers = true;
		} else {
			GetComponent<EcholocationCone> ().revers = false;
		} 

		/*if (Vector2.Angle (playRot, lineRot) > 0.7) {
			GetComponent<EcholocationCone> ().reverse ();

		}*/
		//Debug.Log("player" + Vector3.Angle(playerRotation,lineRotation));
		//Debug.Log("line" + lineRotation.z );

		}
}

