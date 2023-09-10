using UnityEngine;

public class GamepadController : MonoBehaviour {
	public void OnDpadUp () {
		Events.Gamepad.OnDpadUp.Invoke ();
	}
	public void OnDpadDown () {
		Events.Gamepad.OnDpadDown.Invoke ();
	}
	public void OnDpadLeft () {
		Events.Gamepad.OnDpadLeft.Invoke ();
	}
	public void OnDpadRight () {
		Events.Gamepad.OnDpadRight.Invoke ();
	}
}
