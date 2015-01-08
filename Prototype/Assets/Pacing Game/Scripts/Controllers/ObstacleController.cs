using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleController : MonoBehaviour
{
	private static ObstacleController S_instance;
	public static ObstacleController Instance
	{
		get
		{
			if (S_instance == null)
			{
				S_instance = GameObject.Find("Obstacle Controller").GetComponent<ObstacleController>();
			}
			
			return S_instance;
		}
	}
	
	private Queue<GameObject> obstacles;
	
	void Awake()
	{
		obstacles = new Queue<GameObject>();
	}
	
	public void AddObstacle(GameObject g)
	{
		if (!obstacles.Contains(g)) obstacles.Enqueue(g);
	}
	
	public void StartObstacle(float startTime)
	{
		if (obstacles.Count > 0)
		{
			GameObject prefab = obstacles.Dequeue();
			GameObject g = Instantiate(prefab) as GameObject;
			g.audio.time = startTime;
		}
	}
}
