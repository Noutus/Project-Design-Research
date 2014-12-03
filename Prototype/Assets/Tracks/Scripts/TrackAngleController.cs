using UnityEngine;
using System.Collections;

public class TrackAngleController : MonoBehaviour
{
	public float trackAngle;
	
	public float[] curveTimings;
	public float[] curveAngles;
	
	private AudioSource audio;
	
	private float timeSinceStart;
	private float timeToNextCurve;
	private float timeSinceLastCurve;
	private float previousAudioTime;
	
	private int curveIndex;
	
	void Start()
	{
		audio = GameObject.Find("Music").GetComponent<AudioSource>();
		
		previousAudioTime = 0;
		
		curveIndex = 0;
		
		for (int i = 0; i < curveTimings.Length; i++)
		{
			curveTimings[i] = audio.clip.length / curveTimings.Length * i;
			
			int previousIndex = i - 1;
			if (previousIndex < 0) curveAngles[i] = 0;
			else curveAngles[i] = curveAngles[previousIndex] + Random.Range(-45f, 45f);
		}
		
		timeSinceLastCurve = 0;
		timeToNextCurve = curveTimings[curveIndex + 1];
	}
	
	void Update()
	{
		timeSinceLastCurve += audio.time - previousAudioTime;

		float angleTo = 0;
		if (curveIndex + 1 < curveAngles.Length) angleTo = curveAngles[curveIndex + 1]; 
		else angleTo = 0;
		trackAngle = Mathf.Lerp(curveAngles[curveIndex], angleTo, timeSinceLastCurve / timeToNextCurve);

		if (timeSinceLastCurve >= timeToNextCurve)
		{
			SetNextCurve();
		}
		
		previousAudioTime = audio.time;
	}
	
	private void SetNextCurve()
	{
		curveIndex++;
		timeSinceLastCurve = 0;
		timeToNextCurve = curveTimings[curveIndex] - curveTimings[curveIndex - 1];
	}
}
