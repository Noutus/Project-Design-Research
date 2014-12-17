using UnityEngine;
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

	private List<GameObject> trackObjects;

	// Use this to determine the curves in the track.
	public float[] curveAngles;
	
	private MiddlePoint[] points;

	// Howmany points there are in a curve. Adding more will increase the track's length.
	private static int pointsPerCurve = 20;

	void Awake()
	{
		trackObjects = new List<GameObject>();

		points = new MiddlePoint[(curveAngles.Length - 1) * pointsPerCurve];
	}
	
	void Start()
	{

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
	}

	// Shows the track in the scene.
	private void DrawTrack()
	{
		foreach (MiddlePoint m in points)
		{
			//create the line
			GameObject middle = GameObject.Instantiate(pointPrefab, m.Position, Quaternion.Euler(0, 0, m.Angle)) as GameObject;

			//create the right and left wall or every object
			GameObject left = (GameObject) GameObject.Instantiate(wallPrefab, m.Position + Vector2Helper.AngleToVector3(m.Angle + 90) * 5, Quaternion.Euler(0, 0, m.Angle));
			left.transform.localScale = new Vector3(1, Mathf.Cos(m.Angle * Mathf.Deg2Rad) * 3, 1);
			GameObject right=(GameObject) GameObject.Instantiate(wallPrefab,m.Position + Vector2Helper.AngleToVector3(m.Angle - 90) * 5, Quaternion.Euler(0, 0, m.Angle));
			right.transform.localScale = new Vector3(1, 5,1);

			trackObjects.Add(middle);
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
		if (s == "Last") i = points.Length - 1;
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

		trackObjects.Clear();
	}
}
