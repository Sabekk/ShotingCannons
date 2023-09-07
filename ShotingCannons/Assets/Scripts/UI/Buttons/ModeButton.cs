using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ModeButton : UIButton {
	[SerializeField] TMP_Text description;
	[SerializeField] int count;
	[SerializeField] Image cubeImage;
	[SerializeField] Color cubeColor;
	[SerializeField] float choosenScale;
	public override void Initialize () {
		base.Initialize ();
		cubeImage.color = cubeColor;
		description.SetText (count.ToString ());
		ToggleTransition (false);
	}
	private void OnEnable () {
		Events.UI.OnChangeMode += OnChangeMode;
	}
	private void OnDisable () {
		Events.UI.OnChangeMode -= OnChangeMode;
	}

	protected override void OnClick () {
		Events.UI.OnChangeMode.Invoke (count);
	}

	void OnChangeMode (int count) {
		if (this.count == count) {
			transform.localScale = Vector3.one * choosenScale;
			ToggleTransition (true);
		} else {
			ToggleTransition (false);
			transform.localScale = Vector3.one;
		}
	}
}
