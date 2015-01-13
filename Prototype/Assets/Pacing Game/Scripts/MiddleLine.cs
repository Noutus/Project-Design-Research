using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Author: Arnout Verburg

// Readme:
//
// To create a track, fill the curvesAngles array with curves in the inspector.
// See the Test MiddleLine scene for more details of how it's done.
//
// Still needs some changes:
// The last 1,5 curves aren't detected when using the ClosestPoint() method.
// Walls are not implemented yet.

public class MiddleLine : MonoBehaviour
{
	public GameObject pointPrefab;
	public GameObject wallPrefab;
	public GameObject obstaclePrefab;
	public GameObject collectablePrefab;

	private List<GameObject> trackObjects;

	// Use this to determine the curves in the track.
	public float[] curveAngles;
	public int[] obstacleIndexes;
	public int[] collectableIndexes;

	public MiddlePoint[] points;
	private int index;

	// Howmany points there are in a curve. Adding more will increase the track's length.
	private static int pointsPerCurve = 20;

	void Awake()
	{
		trackObjects = new List<GameObject>();

		points = new MiddlePoint[(curveAngles.Length - 1) * pointsPerCurve];
	}

	void Start()
	{
		GameObject[] g = GameObject.FindGameObjectsWithTag("Instrument");
		foreach (GameObject go in g)
		{
			MusicPosition m;
			if (m = go.GetComponent<MusicPosition>()) m.middleLine = this;
		}

		MusicTracker.instance.StartMusic();
		Chaser.Instance.SetNewMiddleLine(this);
	}

	// Creates the track according to the curveAngles array. The track is saved in the points array.
	private void CreateTrack()
	{
		Vector3 v = Vector3.zero;
		float a = curveAngles[0];

		for (int i = 0; i < curveAngles.Length - 1; i++)
		{
			float b = (curveAngles[i + 1] - curveAngles[i]);
			for (int j = 0; j < pointsPerCurve; j++)
			{
				points[i * pointsPerCurve + j] = new MiddlePoint(v, a);
				a += b / pointsPerCurve;
				v += Vector2Helper.AngleToVector3(a);
			}
		}

		for (int k = 0; k < obstacleIndexes.Length; k++)
		{
			Vector3 spawnPosition = points[obstacleIndexes[k]].Position;
			Quaternion spawnRotation = Quaternion.Euler(0, 0, points[obstacleIndexes[k]].Angle);
			GameObject g = Instantiate(obstaclePrefab, spawnPosition, spawnRotation) as GameObject;
		}

		for (int l = 0; l < collectableIndexes.Length; l++)
		{
			Vector3 spawnPosition = points[collectableIndexes[l]].Position;
			Vector2 spawnExtra = Vector2Helper.AngleToVector2(points[collectableIndexes[l]].Angle);
			int r = Mathf.RoundToInt(UnityEngine.Random.value);
			spawnExtra = Vector2Helper.Rotate(spawnExtra, 90 * r - 270 * (r - 1)) * 3;
			spawnPosition.x += spawnExtra.x;
			spawnPosition.y += spawnExtra.y;
			Quaternion spawnRotation = Quaternion.Euler(0, 0, points[collectableIndexes[l]].Angle);
			GameObject g = Instantiate(collectablePrefab, spawnPosition, spawnRotation) as GameObject;
			CollectableController.Instance.AddCollectable(g);
		}
	}

	// Shows the track in the scene.
	private void DrawTrack()
	{
		for (int i = 0; i < points.Length; i++)
		{
			//create the line
			//GameObject middle = GameObject.Instantiate(pointPrefab, m.Position, Quaternion.Euler(0, 0, m.Angle)) as GameObject;

			MiddlePoint m = points[i];
			MiddlePoint p;
			if (i > 0) p = points[i - 1];
			else p = m;

			bool turnRight = false;
			float deltaAngle = p.Angle - m.Angle;
			if (deltaAngle < 0) turnRight = true;

			//create the right and left wall or every object
			GameObject left = (GameObject) GameObject.Instantiate(wallPrefab, m.Position + Vector2Helper.AngleToVector3(m.Angle + 90) * 5, Quaternion.Euler(0, 0, m.Angle));
			if (turnRight) left.transform.localScale = new Vector3(1, Mathf.Sin((deltaAngle + 90) * Mathf.Deg2Rad) * 3, 1);
			else left.transform.localScale = new Vector3(1, Mathf.Cos(deltaAngle * Mathf.Deg2Rad) * 3, 1);
			GameObject right=(GameObject) GameObject.Instantiate(wallPrefab,m.Position + Vector2Helper.AngleToVector3(m.Angle - 90) * 5, Quaternion.Euler(0, 0, m.Angle));
			if (turnRight) right.transform.localScale = new Vector3(1, Mathf.Sin((deltaAngle + 90) * Mathf.Deg2Rad) * 3, 1);
			else right.transform.localScale = new Vector3(1, Mathf.Cos(deltaAngle * Mathf.Deg2Rad) * 3, 1);

			//trackObjects.Add(middle);
			trackObjects.Add(left);
			trackObjects.Add(right);
		}

	}

	// Finds the angle between the middle line and a certain GameObject.
	public float AngleBetween(GameObject g)
	{
		float a = ClosestPoint(g).Angle;
		a -= g.transform.rotation.eulerAngles.z;
		if (a < 0) a = -a;
		return a;
	}

	// Finds the closes point on the track to a certain GameObject.
	public int ClosestIndex(GameObject g)
	{
		int o = 0;
		int p = pointsPerCurve;
		
		// For efficiency, first find the closest point in steps of pointsPerCurve.
		for (int i = 2 * pointsPerCurve; i < points.Length + 1; i += pointsPerCurve)
		{
			int t = i;
			if (t > points.Length - 1) t = points.Length - 1;

			float k = Vector3.Distance(points[p].Position, g.transform.position);
			float l = Vector3.Distance(points[t].Position, g.transform.position);
			
			if (k < l) 
			{
				if (l < Vector3.Distance(points[o].Position, g.transform.position))
				{
					o = p;
					p = t;
				}
				break;
			}
			
			else
			{
				o = p;
				p = t;
			}
		}
		
		// Then search for the closest point in between the nearest steps.
		int m = o;
		
		for (int j = o + 1; j < p; j++)
		{
			float k = Vector3.Distance(points[m].Position, g.transform.position);
			float l = Vector3.Distance(points[j].Position, g.transform.position);
			
			if (k > l) m = j;
		}

		return m;
	}

	public MiddlePoint ClosestPoint(GameObject g)
	{
		int m = ClosestIndex(g);

		return points[m];
	}

	public MiddlePoint GetPointsIndex(int i)
	{
		if (i > points.Length - 1) i = points.Length - 1;
		return points[i];
	}

	public MiddlePoint GetPointsIndex(string s)
	{
		int i = 0;
		if (s == "Last") i = points.Length - 201;

		return points[i];
	}

	public void Load()
	{
		CreateTrack();
		DrawTrack();
	}

	public void Unload()
	{
		foreach (GameObject g in trackObjects)
		{
			Destroy(g);
		}

		MusicTracker.instance.StopMusic();

		trackObjects.Clear();

		Destroy(gameObject);
	}
}
