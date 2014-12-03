using System.Xml;
using System.Xml.Serialization;

public class ShapeExcelEntry
{
	[XmlAttribute("name")]
	public long userID;

	public string currentShape;
	public string chosenShape;

	public ShapeExcelEntry()
	{

	}

	public ShapeExcelEntry(long userID, string currentShape, string chosenShape)
	{
		this.currentShape = currentShape;
		this.chosenShape = chosenShape;
	}
}
