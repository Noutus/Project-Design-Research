using UnityEngine;
using System.Collections;

public class ChaserMovement : MonoBehaviour
{

	private int index;
	public GameObject player;
	public MiddlePoint[] points;
	public float timeDelayed;
	public float moveInterval;

	void Start()
	{
		index = 0;
		player = GameObject.FindGameObjectWithTag("Player");

		points = GetComponent<MiddleLine>().points;
		StartCoroutine(StartMoving());
	}

	void Update()
	{
		Chaser.Instance.SetTo(points[index].Position, points[index].Angle);
	}

	public void increaseIndex()
	{
		Debug.Log("Moving Closer");

		int i= index + 3;
		for(; index < i ;index++)
		{
			int j = index;
			if (j >= points.Length) j = points.Length - 1;
			points[j].Counted = true;
		}
	}

	public void decreaseIndex()
	{
		Debug.Log("Moving Away");

		int i= index - 3;
		for(; index > i ;index--)
		{
			int j = index;
			if (j < 0) j = 0;
			points[j].Counted = true;
		}
	}

	IEnumerator StartMoving()
	{
		yield return new WaitForSeconds(timeDelayed);
		StartCoroutine(MyMethod());
	}

	IEnumerator MyMethod()
	{
		yield return new WaitForSeconds(moveInterval);

		if (index < 0) index = 0;
		if (index >= points.Length) index = points.Length;

		points [index].Counted=true;

		MiddlePoint point = GetComponent<MiddleLine>().ClosestPoint(player);

		int i = 0;

		foreach(MiddlePoint e in points)
		{
			if (e == point) break;
			else i++;
		}
		
		Debug.Log("i: " + i);


		if (point.Counted)
		{
			Debug.Log("THE PLAYER HAS LOST");
		}

		else if ((i - 5) > 0 && points[i - 5].Counted)
		{
			MusicTracker.instance.SwitchTempo(2);
		}

		else if ((i - 10) > 0 && points[i - 10].Counted)
		{
			MusicTracker.instance.SwitchTempo(1);
		}

		else
		{
			MusicTracker.instance.SwitchTempo(0);
		}

		index++;

		StartCoroutine (MyMethod());
	}

}
