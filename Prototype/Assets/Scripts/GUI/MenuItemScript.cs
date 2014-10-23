using UnityEngine;
using System.Collections;

public class MenuItemScript
{
	public string name;

	public MenuItemScript next;
	public MenuItemScript previous;

	public MenuItemScript(string name)
	{
		this.name = name;
	}

	public MenuItemScript(string name, MenuItemScript previous)
	{
		this.name = name;
		this.previous = previous;
	}
}
