using UnityEngine;
using UnityEngine.UI;

public class StartButton : UIButton {
	[SerializeField] GameObject gpad;

	public override void ToggleTransition (bool state) {
		base.ToggleTransition (state);
		gpad.SetActive (state);
	}
}
