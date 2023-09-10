using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ModeButton : UIButton {
	[SerializeField] TMP_Text description;
	[SerializeField] Image cubeImage;
	[SerializeField] Color cubeColor;
	[SerializeField] float choosenScale;
	[SerializeField] GameplayManager.Mode mode;
	public override void Initialize () {
		base.Initialize ();
		cubeImage.color = cubeColor;
		description.SetText (GameplayManager.Instance.GetModeCannonsCount (mode).ToString ());
		ToggleTransition (false);
	}
	private void OnEnable () {
		Events.UI.OnChangeMode += OnChangeMode;
	}
	private void OnDisable () {
		Events.UI.OnChangeMode -= OnChangeMode;
	}

	public override void OnClick () {
		Events.UI.OnChangeMode.Invoke (mode);
	}
	public override void ToggleTransition (bool state) {
		base.ToggleTransition (state);
		if (state == true)
			transform.localScale = Vector3.one * choosenScale;
		else
			transform.localScale = Vector3.one;
	}

	void OnChangeMode (GameplayManager.Mode newMode) {
		if (mode == newMode) {
			ToggleTransition (true);
		} else {
			ToggleTransition (false);
		}
	}
}
