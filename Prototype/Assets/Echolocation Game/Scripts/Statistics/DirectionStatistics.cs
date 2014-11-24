using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class DirectionStatistics : MonoBehaviour
{
	private DirectionExcelData data;

	public float deltaAngle;

	void Start()
	{
		data = new DirectionExcelData();
		Reset();
	}

	public void Reset()
	{
		deltaAngle = 0;
	}

	public void ExportValues()
	{
		data = data.ReadData();
		data.SetData(System.DateTime.Now.Ticks, deltaAngle);
		data.WriteData();
	}
}
