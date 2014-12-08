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

	public MiddlePoint(Vector3 position, float angle)
	{
		this.position = position;
		this.angle = angle;
	}
}
