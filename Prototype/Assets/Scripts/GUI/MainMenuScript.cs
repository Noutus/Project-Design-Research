using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuScript : MonoBehaviour
{
	private bool upPressed;
	private bool downPressed;

	protected bool up;
	protected float t;
	protected float startHeight;
	protected Rect startPosition;
	
	public string[] menuItemNames;
	public string[] menuItemRefs;
	public bool[] menuItemLocks;

	public GUIStyle activeStyle;
	public GUIStyle passiveStyle;
	public GUIStyle lockedStyle;

	private MenuItemScript[] menuItems;
	protected MenuItemScript[] MenuItems
	{
		get { return menuItems; }
		set { menuItems = value; }
	}

	private MenuItemScript activeItem;
	protected MenuItemScript ActiveItem
	{
		get {return activeItem; }
		set {activeItem = value; }
	}

	void Awake()
	{
		up = false;
		t = 1;
		startHeight = Screen.height / 2;
		startPosition = new Rect(0, startHeight, Screen.width, 50);
	}

	void Start()
	{
		Initialize();
	}

	void Update()
	{
		if (Input.GetAxis("MenuNavigate") > 0.1f)
		{
			upPressed = false;
			downPressed = true;
		}
		else if (Input.GetAxis("MenuNavigate") < -0.1f)
		{
			downPressed = false;
			upPressed = true;
		}
		else
		{
			upPressed = false;
			downPressed = false;
		}

		if (upPressed && t == 1 && activeItem != menuItems[0])
		{
			activeItem = activeItem.previous;
			up = true;
			t = 0;
		}

		else if (downPressed && t == 1 && activeItem != menuItems[menuItems.Length - 1])
		{
			activeItem = activeItem.next;
			up = false;
			t = 0;
		}

		else if (t == 1 && Input.GetKeyDown(KeyCode.UpArrow) && activeItem != menuItems[0])
		{
			activeItem = activeItem.previous;
			up = true;
			t = 0;
		}

		else if (t == 1 && Input.GetKeyDown(KeyCode.DownArrow) && activeItem != menuItems[menuItems.Length - 1])
		{
			activeItem = activeItem.next;
			up = false;
			t = 0;
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetAxis("MenuSelect") > 0)
		{
			for (int i = 0; i < menuItemNames.Length; i++)
			{
				if (activeItem.name == menuItemNames[i])
				{
					Application.LoadLevel(menuItemRefs[i]);
				}
			}
		}

		t += Time.deltaTime * 4;
		if (t >= 1) t = 1;

		if (up)
			startHeight = Mathf.Lerp(Screen.height / 2 - startPosition.height * 1.2f, Screen.height / 2, t);
		else
			startHeight = Mathf.Lerp(Screen.height / 2 + startPosition.height * 1.2f, Screen.height / 2, t);

		startPosition = new Rect(0, startHeight, Screen.width, 50);
	}

	void OnGUI()
	{
		Rect position = startPosition;
		bool foundActiveItem = false;
		Stack previousItems = new Stack();

		for (int i = 0; i < menuItems.Length; i++)
		{
			if (foundActiveItem)
			{
				Rect nextPosition = position;
				nextPosition.y += startPosition.height * i * 1.2f;
				if (menuItems[i].locked)
				{
					GUI.TextArea(nextPosition, menuItems[i].name, lockedStyle);
				}
				else
				{
					GUI.TextArea(nextPosition, menuItems[i].name, passiveStyle);
				}
			}

			else
			{
				if (menuItems[i] == activeItem)
				{
					MenuItemScript previous;
					while (previousItems.Count > 0)
					{
						previous = (MenuItemScript) previousItems.Pop();
						position.y -= 1.2f * startPosition.height;
						if (previous.locked)
						{
							GUI.TextArea(position, previous.name, lockedStyle);
						}
						else
						{
							GUI.TextArea(position, previous.name, passiveStyle);
						}
					}

					if (activeItem.locked)
					{
						GUI.TextArea(startPosition, activeItem.name, lockedStyle);
					}
					else
					{
						GUI.TextArea(startPosition, activeItem.name, activeStyle);
					}
					foundActiveItem = true;
				}

				else
				{
					previousItems.Push(menuItems[i]);
				}
		    }
		}
	}

	public void Initialize()
	{
		menuItems = new MenuItemScript[menuItemNames.Length];

		for (int i = 0; i < menuItemNames.Length; i++)
		{
			menuItems[i] = new MenuItemScript(menuItemNames[i]);
			menuItems[i].locked = menuItemLocks[i];
		}

		for (int j = 0; j < menuItems.Length; j++)
		{
			menuItems[j].previous = menuItems[(j + menuItems.Length - 1) % menuItems.Length];
			menuItems[j].next = menuItems[(j + menuItems.Length + 1) % menuItems.Length];
		}

		activeItem = menuItems[0];
	}
}
