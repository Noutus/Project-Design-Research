using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class ShapeStatistics : MonoBehaviour
{
	private ShapeExcelData data;

	public string playMode;
	public string currentShape;
	public string chosenShape;

	void Start()
	{
		data = new ShapeExcelData();
		Reset();
	}

	public void Reset()
	{
		playMode = "";
		currentShape = "";
		chosenShape = "";
	}

	public void ExportValues()
	{
		data = data.ReadData();
		data.SetData(System.DateTime.Now.Ticks, playMode, currentShape, chosenShape);
		data.WriteData();
	}
}
