using UnityEngine;

public abstract class ViewBase : MonoBehaviour {
	[SerializeField] GameObject view;

	public const string Dpad_UP = "DpadUp";
	public const string Dpad_DOWN = "DpadDown";
	public const string Dpad_LEFT = "DpadLeft";
	public const string Dpad_RIGHT = "DpadRight";
	public virtual void Activate () {
		view.SetActive (true);
		Events.Gamepad.OnDpadUp += OnDpadUp;
		Events.Gamepad.OnDpadDown += OnDpadDown;
		Events.Gamepad.OnDpadLeft += OnDpadLeft;
		Events.Gamepad.OnDpadRight += OnDpadRight;
	}
	public virtual void Deactivate () {
		view.SetActive (false);
		Events.Gamepad.OnDpadUp -= OnDpadUp;
		Events.Gamepad.OnDpadDown -= OnDpadDown;
		Events.Gamepad.OnDpadLeft -= OnDpadLeft;
		Events.Gamepad.OnDpadRight -= OnDpadRight;
	}

	public virtual void OnDpadAction (string type) {

	}

	void OnDpadUp () {
		OnDpadAction (Dpad_UP);
	}
	void OnDpadDown () {
		OnDpadAction (Dpad_DOWN);
	}
	void OnDpadLeft () {
		OnDpadAction (Dpad_LEFT);
	}
	void OnDpadRight () {
		OnDpadAction (Dpad_RIGHT);
	}

}
