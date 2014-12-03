using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour
{
	GameObject player;

	public string nextLevel;

	void Start()
	{
		player = GameObject.Find("Player");
	}

	void Update()
	{
		if (Vector3.Distance(player.transform.position, transform.position) < 5)
		{
			GameObject go;
			if (go = GameObject.Find("Statistics Line"))
			{
				go.GetComponent<PlayerLineScript>().ExportTexture();
			}

			if (go = GameObject.Find("Statistics"))
			{
				go.GetComponent<StatisticsValueScript>().ExportValues();
			}

			if (Application.CanStreamedLevelBeLoaded(nextLevel))
			{
				Application.LoadLevel(nextLevel);
			}

			else
			{
				Application.LoadLevel("Menu Main");
			}
		}
	}
}
