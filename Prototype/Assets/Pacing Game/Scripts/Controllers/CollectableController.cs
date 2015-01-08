using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectableController : MonoBehaviour
{
	private static CollectableController S_instance;
	public static CollectableController Instance
	{
		get
		{
			if (S_instance == null)
			{
				S_instance = GameObject.Find("Collectable Controller").GetComponent<CollectableController>();
			}

			return S_instance;
		}
	}

	private List<GameObject> collectables;

	void Awake()
	{
		collectables = new List<GameObject>();
	}

	public void AddCollectable(GameObject g)
	{
		collectables.Add(g);
	}

	public void RemoveCollectable(GameObject g)
	{
		if (collectables.Contains(g)) collectables.Remove(g);
	}

	public void SetCollectableTimings(float t)
	{
		foreach (GameObject g in collectables)
		{
			if (g != null) g.audio.time = t;
		}
	}
}
