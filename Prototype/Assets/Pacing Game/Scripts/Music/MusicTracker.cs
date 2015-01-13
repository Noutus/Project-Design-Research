using UnityEngine;
using System.Collections;

public class MusicTracker : MonoBehaviour
{
	private static MusicTracker s_Instance = null;
	public static MusicTracker instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = GameObject.Find("Music Tracker").GetComponent<MusicTracker>();
			}
			
			return s_Instance;
		}
	}

	private MusicState state;
	public enum MusicState
	{
		Idle,
		Intro,
		Phase,
		LastPhase,
		Transition,
		Gameover,
	}

	public GameObject[] musicSystemPrefabs;
	private GameObject[] musicSystems;
	public GameObject[] ActiveSystem
	{
		get
		{
			return new GameObject[]{musicSystems[systemIndex], musicSystems[systemIndex].GetComponent<MusicSystem>().badMusic};
		}
	}

	private int systemIndex;

	private float musicTime;
	public float MusicTime { get { return musicTime; }}
	private int phaseIndex;
	public int PhaseIndex { get { return phaseIndex; }}

	public bool endLevel = false;
	public bool gameOver = false;

	private float timeSinceLastStateSwitch = 0;

	void Awake()
	{
		state = MusicState.Idle;
		musicTime = 0;
		phaseIndex = 0;
	}

	void Start()
	{
		musicSystems = new GameObject[musicSystemPrefabs.Length];
		for (int i = 0; i < musicSystems.Length; i++)
		{
			musicSystems[i] = GameObject.Instantiate(musicSystemPrefabs[i]) as GameObject;
		}
	}

	void Update()
	{
		switch (state)
		{
		case MusicState.Idle:
			break;
		case MusicState.Intro:
			if (timeSinceLastStateSwitch > 2.343)
			{
				SwitchState(MusicState.Phase);
			}
			break;
		case MusicState.Phase:
			if (timeSinceLastStateSwitch > 15)
			{
				if (endLevel)
				{
					endLevel = false;
					SwitchState(MusicState.LastPhase);
				}

				else
				{
					StartNextPhase();
				}
			}
			break;
		case MusicState.LastPhase:
			if (timeSinceLastStateSwitch > 15)
			{
				SwitchState(MusicState.Transition);
			}
			break;
		case MusicState.Transition:
			if (timeSinceLastStateSwitch > 13.124)
			{
				SwitchState(MusicState.Phase);
				PacingFinish.instance.NewLevel(true);
			}
			break;
		case MusicState.Gameover:
			gameOver = false;
			if (timeSinceLastStateSwitch > 3.750)
			{
				state = MusicState.Idle;
				SoundMenu.Instance.ReturnToMenu();
			}
			break;
		}

		if (timeSinceLastStateSwitch % 1.875 < Time.deltaTime)
		{
			if (gameOver)
			{
				gameOver = false;
				SwitchState(MusicState.Gameover);
			}

			else
			{
				ObstacleController.Instance.StartObstacle((float)(timeSinceLastStateSwitch % 1.875));
			}
		}

		timeSinceLastStateSwitch += Time.deltaTime;
		musicTime = musicSystems[systemIndex].audio.time + Time.deltaTime;
	}

	public void StartLevel()
	{
		timeSinceLastStateSwitch = 0;

		systemIndex = 0;
		state = MusicState.Intro;

		StartMusic();
	}

	public void StartNextPhase()
	{
		timeSinceLastStateSwitch = 0;
		phaseIndex = (phaseIndex + 1) % 3;
		StartMusic();
	}

	public void StopMusic()
	{
		musicSystems[systemIndex].GetComponent<MusicSystem>().StopMusic();
	}

	public void StopPreviousMusic(int i)
	{
		musicSystems[i].GetComponent<MusicSystem>().StopMusic();
	}

	public void StartMusic()
	{
		musicSystems[systemIndex].GetComponent<MusicSystem>().PlayMusic(state, phaseIndex, timeSinceLastStateSwitch);
	}

	public void SwitchTempo(int i)
	{
		int j = systemIndex;

		systemIndex = i;

		CollectableController.Instance.SetHeight(i, musicTime);
		PacingSystem p = GameObject.FindGameObjectWithTag("Player").GetComponent<PacingSystem>();
		p.Instruments = ActiveSystem;

		StartMusic();

		if (i != j) StopPreviousMusic(j);
	}

	private void SwitchState(MusicState s)
	{
		state = s;

		timeSinceLastStateSwitch = 0;

		StartMusic();
	}
}
