using System.Xml;
using System.Xml.Serialization;

public class ShapeExcelEntry
{
	[XmlAttribute("name")]
	public long userID;

	public string playMode;
	public string currentShape;
	public string chosenShape;

	public ShapeExcelEntry()
	{

	}

	public ShapeExcelEntry(long userID, string playMode, string currentShape, string chosenShape)
	{
		this.playMode = playMode;
		this.currentShape = currentShape;
		this.chosenShape = chosenShape;
	}
}
