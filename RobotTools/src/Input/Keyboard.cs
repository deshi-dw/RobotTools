namespace RobotTools
{
	public class Keyboard
	{
		public InputDevice Device { get; set; }

		public bool IsKeyDown(Keycode key) { return false; }
		public bool IsKeyUp(Keycode key) { return false; }
		public bool IsKeyHeld(Keycode key) { return false; }

		public Keycode[] GetHeldKeys(Keycode key) { return null; }
	}
}