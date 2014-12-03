using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Data")]
public class ShapeExcelData
{
	[XmlArray("Users"), XmlArrayItem("User")]
	public List<ShapeExcelEntry> entries = new List<ShapeExcelEntry>();

	public ShapeExcelData ReadData()
	{
		string path = Application.dataPath + "/../Data/EchoShapeData.xml";

		var serializer = new XmlSerializer(typeof(ShapeExcelData));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as ShapeExcelData;
		}
	}
	
	public void WriteData()
	{
	string path = Application.dataPath + "/../Data/EchoShapeData.xml";
		
		XmlSerializer serializer = new XmlSerializer(typeof(ShapeExcelData));
		FileStream stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}

	public void SetData(long userID, string playMode, string currentShape, string chosenShape)
	{
		entries.Add(new ShapeExcelEntry(userID, playMode, currentShape, chosenShape));
	}
}
