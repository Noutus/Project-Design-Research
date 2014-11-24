using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class StatisticsValueScript : MonoBehaviour
{
	ExcelData data;

	// Player Statistics
	public int turnAmount;
	public int strafeAmount;
	public float upTime;
	public float turnTime;
	public float strafeTime;
	public float idleTime;

	// Music Statistics
	public float reverseTime;
	public float wallTime;

	public float averageAngle;
	public float totalTime;

	void Start()
	{
		data = new ExcelData();
		Reset();
	}

	public void Reset()
	{
		turnAmount = 0;
		strafeAmount = 0;
		upTime = 0;
		turnTime = 0;
		strafeTime = 0;
		idleTime = 0;
	}

	public void ExportValues()
	{
		data = data.ReadData();
		data.SetData(System.DateTime.Now.Ticks, Application.loadedLevelName, turnAmount, strafeAmount, upTime, turnTime, strafeTime, idleTime, reverseTime, wallTime, averageAngle / totalTime, totalTime);
		data.WriteData();
	}
}
