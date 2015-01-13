using UnityEngine;
using System.Collections;

public class ChaserMovement : MonoBehaviour
{
	public MiddlePoint[] points;

	public GameObject player;

	public float timeDelayed;
	public float moveInterval;

	private int index;

	public bool started = false;

	void Awake()
	{
		index = 0;
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		points = GetComponent<MiddleLine>().points;
	}
	
	void Update()
	{
		Chaser.Instance.SetTo(points[index].Position, points[index].Angle);
	}

	public void StartMove()
	{
		started = true;
		StartCoroutine(StartMoving());
	}

	public void increaseIndex()
	{
		for (int i = index + 3; index < i ;index++)
		{
			int j = index;
			if (j >= points.Length) j = points.Length - 1;
			points[j].Counted = true;
		}
	}

	public void decreaseIndex()
	{
		int i= index - 10;
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

		points[index].Counted = true;

		MiddlePoint point = GetComponent<MiddleLine>().ClosestPoint(player);

		int i = 0;

		foreach(MiddlePoint e in points)
		{
			if (e == point) break;
			else i++;
		}

		if (point.Counted) MusicTracker.instance.gameOver = true;
		else if ((i - 5) > 0 && points[i - 5].Counted) MusicTracker.instance.SwitchTempo(2);
		else if ((i - 10) > 0 && points[i - 10].Counted) MusicTracker.instance.SwitchTempo(1);
		else MusicTracker.instance.SwitchTempo(0);

		index++;

		StartCoroutine (MyMethod());
	}

}
