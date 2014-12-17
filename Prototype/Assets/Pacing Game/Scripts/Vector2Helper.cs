using UnityEngine;
using System.Collections;

public class Vector2Helper : MonoBehaviour
{
	// Source: http://answers.unity3d.com/questions/661383/whats-the-most-efficient-way-to-rotate-a-vector2-o.html
	public static Vector2 Rotate(Vector2 vector, float angle)
	{
		float _x = vector.x;
		float _y = vector.y;
		
		float _angle = angle * Mathf.Deg2Rad;
		float _cos = Mathf.Cos(_angle);
		float _sin = Mathf.Sin (_angle);
		
		float _x2 = _x * _cos - _y * _sin;
		float _y2 = _x * _sin + _y * _cos;
		
		return new Vector2(_x2, _y2);
	}

	public static Vector2 AngleToVector2(float angle)
	{
		return Rotate(Vector2.up, angle);
	}

	public static Vector3 AngleToVector3(float angle)
	{
		Vector2 v = AngleToVector2(angle);
		return new Vector3(v.x, v.y, 0);
	}
}
