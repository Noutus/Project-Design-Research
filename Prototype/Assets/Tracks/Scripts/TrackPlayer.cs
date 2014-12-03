using UnityEngine;
using System.Collections;

public class TrackPlayer : MonoBehaviour
{
	public static float rotateAngle = 50;
	public static float maxDistance = 3;

	private TrackAngleController angleController;
	private TrackMusic music;

	void Start()
	{
		angleController = GameObject.Find("Angle Controller").GetComponent<TrackAngleController>();
		music = GameObject.Find("Music").GetComponent<TrackMusic>();
	}
	
	void Update()
	{
		Rotate();
		Move();
	}

	private void Rotate()
	{
		float angle = transform.rotation.eulerAngles.z - angleController.trackAngle;
		
		if (Input.GetKey(KeyCode.A))
		{
			if (angle > 180) angle -= 360;
			if (angle + rotateAngle * Time.deltaTime < 90) transform.Rotate(Vector3.forward, rotateAngle * Time.deltaTime);
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			if (angle < 180) angle += 360;
			if (angle - rotateAngle * Time.deltaTime > 270) transform.Rotate(Vector3.forward, -rotateAngle * Time.deltaTime);
		}
	}

	private void Move()
	{
		Vector2 direction = Vector2Helper.AngleToVector2(transform.rotation.eulerAngles.z);

		Vector3 newPosition = transform.position + new Vector3(direction.x, music.moveDirection.y) * music.DeltaMusicTime;

		if (Vector3.Distance(newPosition, music.transform.position - music.moveDirection) > maxDistance)
		{
			newPosition = transform.position + music.moveDirection * music.DeltaMusicTime;
		}

		transform.position = newPosition;
	}
}

