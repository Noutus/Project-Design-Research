using UnityEngine;
using System.Collections;
	
public class MusicSave
{
	private int level;
	public int Level
	{
		get { return level; }
	}
	
	public MusicSave()
	{
		
	}
	
	public MusicSave(int level)
	{
		this.level = level;
	}
}
