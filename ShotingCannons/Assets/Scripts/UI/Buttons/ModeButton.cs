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
		description.SetText (GameplayManager.Instance.GetModeCannonsCount(mode).ToString ());
		ToggleTransition (false);
	}
	private void OnEnable () {
		Events.UI.OnChangeMode += OnChangeMode;
	}
	private void OnDisable () {
		Events.UI.OnChangeMode -= OnChangeMode;
	}

	protected override void OnClick () {
		Events.UI.OnChangeMode.Invoke (mode);
	}

	void OnChangeMode (GameplayManager.Mode newMode) {
		if (mode == newMode) {
			transform.localScale = Vector3.one * choosenScale;
			ToggleTransition (true);
		} else {
			ToggleTransition (false);
			transform.localScale = Vector3.one;
		}
	}
}
