using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
	private bool up;
	private float t;
	private float startHeight;
	private Rect startPosition;

	public GUIStyle activeStyle;
	public GUIStyle passiveStyle;

	private MenuItemScript[] menuItems;
	private MenuItemScript activeItem;

	void Start()
	{
		up = false;
		t = 1;
		startHeight = Screen.height / 2;
		startPosition = new Rect(Screen.width / 2, startHeight, 100, 50);

		Initialize();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			activeItem = activeItem.previous;
			up = true;
			t = 0;
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			activeItem = activeItem.next;
			up = false;
			t = 0;
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (activeItem.name == "Play")
			{
				Application.LoadLevel("Test Echolocation");
			}
		}

		t += Time.deltaTime * 4;
		if (t >= 1)
			t = 1;

		if (up)
			startHeight = Mathf.Lerp(Screen.height / 2 - startPosition.height * 1.2f, Screen.height / 2, t);
		else
			startHeight = Mathf.Lerp(Screen.height / 2 + startPosition.height * 1.2f, Screen.height / 2, t);

		startPosition = new Rect(0, startHeight, Screen.width / 2, 50);
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
				GUI.TextArea(nextPosition, menuItems[i].name, passiveStyle);
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
						GUI.TextArea(position, previous.name, passiveStyle);
					}

					GUI.TextArea(startPosition, activeItem.name, activeStyle);
					foundActiveItem = true;
				}

				else
				{
					previousItems.Push(menuItems[i]);
				}
		    }
		}
	}

	private void Initialize()
	{
		string[] menuItemNames = new string[] {"Play", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7", "Option 8"};

		menuItems = new MenuItemScript[menuItemNames.Length];

		for (int i = 0; i < menuItemNames.Length; i++)
			menuItems[i] = new MenuItemScript(menuItemNames[i]);

		for (int j = 0; j < menuItems.Length; j++)
		{
			menuItems[j].previous = menuItems[(j + menuItems.Length - 1) % menuItems.Length];
			menuItems[j].next = menuItems[(j + menuItems.Length + 1) % menuItems.Length];
		}

		activeItem = menuItems[0];
	}
}
