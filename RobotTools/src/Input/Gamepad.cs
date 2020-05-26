namespace RobotTools
{
	public class Gamepad
	{
		public InputDevice Device { get; set; }

		public double GetAxis(GamepadAxis axis) { return 0.0; }

		public bool IsButtonDown(GamepadButton button) { return false; }
		public bool IsButtonUp(GamepadButton button) { return false; }
		public bool IsButtonHeld(GamepadButton button) { return false; }
	}
}