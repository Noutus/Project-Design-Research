using System.Xml;
using System.Xml.Serialization;

public class DirectionExcelEntry
{
	[XmlAttribute("name")]
	public long userID;

	public float deltaAngle;

	public DirectionExcelEntry()
	{

	}

	public DirectionExcelEntry(long userID, float deltaAngle)
	{
		this.deltaAngle = deltaAngle;
	}
}
