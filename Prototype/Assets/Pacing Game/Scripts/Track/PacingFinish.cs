using UnityEngine;
using System.Collections;

public class PacingFinish : MonoBehaviour
{
	private MiddleLine track;

	public GameObject[] trackPrefabs;

	public GameObject playerPrefab;
	private GameObject player;

	public GameObject audioListener;

	private int index;

	void Update()
	{
		if (player != null) audioListener.transform.position = player.transform.position;
	}

	public void Initialize()
	{
		index = MusicGlobals.instance.Level;

		LoadLevel(index);
	}

	void OnTriggerEnter()
	{
		// Unload old level
		track.Unload();

		// Load new level
		LoadLevel(index);
	}

	public void LoadLevel(int i)
	{
		GameObject g = GameObject.Instantiate(trackPrefabs[i], Vector3.zero, Quaternion.identity) as GameObject;
		track = g.GetComponent<MiddleLine>();
		track.Load();

		if (player == null)
		{
			player = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		}

		else
		{	
			player.transform.position = Vector3.zero;
			player.transform.rotation = Quaternion.identity;
		}

		player.GetComponent<PlayerController>().SetTrack(g);

		MiddlePoint p = track.GetPointsIndex("Last");
		transform.position = p.Position;
		transform.rotation = Quaternion.Euler(0, 0, p.Angle);
	}
}
