namespace RobotTools
{
	public class Mouse
	{
		public InputDevice Device { get; set; }

		public double GetX() { return 0.0; }
		public double GetY() { return 0.0; }

		public double GetAccelerationX() { return 0.0; }
		public double GetAccelerationY() { return 0.0; }

		public bool IsButtonDown(MouseButton button) { return false; }
		public bool IsButtonUp(MouseButton button) { return false; }
		public bool IsButtonHeld(MouseButton button) { return false; }
	}
}