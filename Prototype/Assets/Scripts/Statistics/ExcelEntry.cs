using System.Xml;
using System.Xml.Serialization;

public class ExcelEntry
{
	[XmlAttribute("name")]
	public long userID;

	public string levelName;

	// Player Statistics
	public int turnAmount;
	public int strafeAmount;
	public float upTime;
	public float turnTime;
	public float strafeTime;
	public float idleTime;
	public float reverseTime;
	public float wallTime;
	public float averageAngle;
	public float totalTime;

	public ExcelEntry()
	{

	}

	public ExcelEntry(long userID, string levelName, int turnAmount, int strafeAmount, float upTime, float turnTime, float strafeTime, float idleTime, float reverseTime, float wallTime, float averageAngle, float totalTime)
	{
		this.userID = userID;
		this.levelName = levelName;
		this.turnAmount = turnAmount;
		this.strafeAmount = strafeAmount;
		this.upTime = upTime;
		this.turnTime = turnTime;
		this.strafeTime = strafeTime;
		this.idleTime = idleTime;
		this.reverseTime = reverseTime;
		this.wallTime = wallTime;
		this.averageAngle = averageAngle;
		this.totalTime = totalTime;
	}
}
