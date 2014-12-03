using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

// Author: Arnout Verburg

public class GlobalValues : MonoBehaviour
{
	// The number of levels unlocked by the player
	// Data is loaded as the game starts
	private int levelsUnlocked;
	public int LevelsUnlocked
	{
		get { return levelsUnlocked; }
		set { levelsUnlocked = value; }
	}

	// GlobalValues is an instance of an object. That means only one of it exists in the game
	// The first time it is accessed, it creates a new GameObject that represents the GlobalValues
	private static GlobalValues s_Instance = null;
	public static GlobalValues instance
	{
		get
		{
			if (s_Instance == null)
			{
				// Create the gameobject for the GlobalValues
				GameObject go = new GameObject("Global Values");
				s_Instance = (GlobalValues) go.AddComponent(typeof(GlobalValues));
				s_Instance.LoadGame();
			}

			return s_Instance;
		}
	}

	void Start()
	{
		// Set GameObject to stay when switching scenes
		DontDestroyOnLoad(this);
	}

	public void LoadGame()
	{
		// insert code for loading game
		SaveGame data = new global::SaveGame();
		Stream stream = File.Open("BatSave.game", FileMode.Open);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder();
		data = (SaveGame) bformatter.Deserialize(stream);
		stream.Close();

		levelsUnlocked = data.LevelsUnlocked;
		if (levelsUnlocked <= 0)
		{
			levelsUnlocked = 0;
		}
	}

	public void SaveGame()
	{
		// insert code for saving game
		SaveGame data = new global::SaveGame(levelsUnlocked);

		Stream stream = File.Open("BatSave.game", FileMode.Create);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder();
		bformatter.Serialize(stream, data);
		stream.Close();
	}
}

[Serializable]
public class SaveGame
{
	private int levelsUnlocked;
	public int LevelsUnlocked
	{
		get { return levelsUnlocked; }
	}

	public SaveGame()
	{

	}

	public SaveGame(int levelsUnlocked)
	{
		this.levelsUnlocked = levelsUnlocked;
	}
}

public sealed class VersionDeserializationBinder : SerializationBinder
{
	public override Type BindToType(string assemblyName, string typeName)
	{
		if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
		{
			Type typeToDeserialize = null;

			assemblyName = Assembly.GetExecutingAssembly().FullName; 
			
			// The following line of code returns the type. 
			typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName)); 
			
			return typeToDeserialize; 
		} 
		
		return null; 
	}
}
