using UnityEngine;

public class MainMenu : ViewBase {
	[SerializeField] ModeButton[] buttons;
	[SerializeField] StartButton startButton;

	GameplayManager.Mode choosenMode;
	public bool AwailableToStart => choosenMode != GameplayManager.Mode.none;

	ModeButton selectedButton;
	private void OnEnable () {
		Events.UI.OnChangeMode += OnChangeCountToSpawn;
	}
	private void OnDisable () {
		Events.UI.OnChangeMode -= OnChangeCountToSpawn;
	}

	protected override void Initialize () {
		foreach (var button in buttons) {
			button.Initialize ();
		}
		startButton.Initialize ();
	}

	public override void Activate () {
		base.Activate ();
		choosenMode = GameplayManager.Mode.none;
		foreach (var button in buttons) {
			button.ToggleTransition (false);
		}

		Refresh ();
	}

	void OnChangeCountToSpawn (GameplayManager.Mode newMode) {
		choosenMode = newMode;
		Refresh ();
	}

	public void StartGame () {
		if (!AwailableToStart)
			return;
		else {
			Events.Gameplay.OnStartGame.Invoke (choosenMode);
			selectedButton = null;
		}
	}

	void Refresh () {
		startButton.ToggleTransition (AwailableToStart);
	}

	void CycleButtons (int dir) {
		int currentIndex = 0;
		if (selectedButton == null) {
			if (buttons.Length == 0)
				return;
			else
				selectedButton = buttons[0];
		} else {
			for (int i = 0; i < buttons.Length; i++) {
				if (buttons[i] == selectedButton) {
					currentIndex = i;
					break;
				}
			}
			currentIndex += dir;
			if (currentIndex >= buttons.Length)
				currentIndex = 0;
			if (currentIndex < 0)
				currentIndex = buttons.Length - 1;
		}

		selectedButton = buttons[currentIndex];
		selectedButton.OnClick ();
	}

	public override void OnDpadAction (string type) {
		base.OnDpadAction (type);
		switch (type) {
			case Dpad_RIGHT:
			StartGame ();
			break;
			case Dpad_UP:
			CycleButtons (-1);
			break;
			case Dpad_DOWN:
			CycleButtons (1);
			break;
			default:
			break;
		}
	}
}
