namespace RobotTools
{
	public abstract class HidMapperPlugin : Plugin
	{
		public abstract bool[] MapButtons(byte[] rawData);
		public abstract double[] MapAxis(byte[] rawData);
	}
}
