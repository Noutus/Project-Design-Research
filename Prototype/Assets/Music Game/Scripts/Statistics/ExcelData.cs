using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Data")]
public class ExcelData
{
	[XmlArray("Users"), XmlArrayItem("User")]
	public List<ExcelEntry> entries = new List<ExcelEntry>();

	public ExcelData ReadData()
	{
		string path = Application.dataPath + "/../Data/Data.xml";

		var serializer = new XmlSerializer(typeof(ExcelData));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as ExcelData;
		}
	}
	
	public void WriteData()
	{
	string path = Application.dataPath + "/../Data/Data.xml";
		
		XmlSerializer serializer = new XmlSerializer(typeof(ExcelData));
		FileStream stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}

	public void SetData(long userID, string levelName, int turnAmount, int strafeAmount, float upTime, float turnTime, float strafeTime, float idleTime, float reverseTime, float wallTime, float averageAngle, float totalTime)
	{
		entries.Add(new ExcelEntry(userID, levelName, turnAmount, strafeAmount, upTime, turnTime, strafeTime, idleTime, reverseTime, wallTime, averageAngle, totalTime));
	}
}
