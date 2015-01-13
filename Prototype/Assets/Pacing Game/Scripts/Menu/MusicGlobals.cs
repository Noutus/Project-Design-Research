using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

// Author: Arnout Verburg

public class MusicGlobals : MonoBehaviour
{
	private static MusicGlobals s_Instance = null;
	public static MusicGlobals instance
	{
		get
		{
			if (s_Instance == null)
			{
				GameObject go = new GameObject("MusicGlobals");
				s_Instance = (MusicGlobals) go.AddComponent(typeof(MusicGlobals));
				//s_Instance.LoadGame();
			}

			return s_Instance;
		}
	}

	private const string path = "SaveGame.game";

	private int level;
	public int Level
	{
		get { return level; }
		set { level = value; }
	}

	void Start()
	{
		DontDestroyOnLoad(this);
	}

	public void LoadGame()
	{
		MusicSave data = new MusicSave();

		Stream stream = File.Open(path, FileMode.OpenOrCreate);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder();

		if (stream.Length > 0)
		{
			data = (MusicSave)bformatter.Deserialize(stream);
			level = data.Level;
		}

		else level = 0;

		stream.Close();
	}

	public void SaveGame()
	{
		MusicSave data = new MusicSave(level);

		Stream stream = File.Open(path, FileMode.Create);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder();
		bformatter.Serialize(stream, data);
		stream.Close();
	}
}
