using UnityEngine;
using System.Collections;

public class Chaser : MonoBehaviour
{
	public GameObject player;
	public MiddleLine middleLine;
	
	private Vector3 from;
	private Vector3 to;
	private Vector3 previousTo;

	private float fromAngle;
	private float toAngle;
	private float previousToAngle;

	private float lerpTime;

	private static Chaser S_instance;
	public static Chaser Instance
	{
		get
		{
			if (S_instance == null)
			{
				S_instance = GameObject.FindGameObjectWithTag("Chaser").GetComponent<Chaser>();
			}

			return S_instance;
		}
	}

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
			if (lerpTime < 1)
			{
				lerpTime += Time.deltaTime / 0.4f;
			}

			if (toAngle != previousToAngle || to != previousTo)
			{
				from = previousTo;
				fromAngle = previousToAngle;
				lerpTime = 0;
			}
			
			transform.position = Vector3.Lerp(from, to, lerpTime);
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(fromAngle, toAngle, lerpTime));

			previousTo = to;
			previousToAngle = toAngle;
		}
	}

	public void SetTo(Vector3 v, float a)
	{
		to = v;
		toAngle = a;
	}

	public void SetNewMiddleLine(MiddleLine m)
	{
		middleLine = m;
	}

	public void SetNewPlayer(GameObject g)
	{
		player = g;
	}
}

