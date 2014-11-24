using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Data")]
public class DirectionExcelData
{
	[XmlArray("Users"), XmlArrayItem("User")]
	public List<DirectionExcelEntry> entries = new List<DirectionExcelEntry>();

	public DirectionExcelData ReadData()
	{
		string path = Application.dataPath + "/../Data/EchoAngleData.xml";

		var serializer = new XmlSerializer(typeof(DirectionExcelData));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as DirectionExcelData;
		}
	}
	
	public void WriteData()
	{
	string path = Application.dataPath + "/../Data/EchoAngleData.xml";
		
		XmlSerializer serializer = new XmlSerializer(typeof(DirectionExcelData));
		FileStream stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}

	public void SetData(long userID, float deltaAngle)
	{
		entries.Add(new DirectionExcelEntry(userID, deltaAngle));
	}
}
