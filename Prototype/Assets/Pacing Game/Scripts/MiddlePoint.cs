using UnityEngine;

public class MiddlePoint
{
	private Vector3 position;
	public Vector3 Position
	{
		get { return position; }
		set { position = value; }
	}

	private float angle;
	public float Angle
	{
		get { return angle; }
		set { angle = value; }
	}
	private bool counted;
	public bool Counted
	{
		get { return counted; }
		set { counted = value; }
	}

	public MiddlePoint(Vector3 position, float angle)
	{
		this.position = position;
		this.angle = angle;
		this.counted = false;
	}
}
