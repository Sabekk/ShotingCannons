using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : ViewBase {
	[SerializeField] ModeButton[] buttons;
	[SerializeField] StartButton startButton;

	GameplayManager.Mode choosenMode;
	public bool AwailableToStart => choosenMode != GameplayManager.Mode.none;
	private void OnEnable () {
		Initialize ();
		Events.UI.OnChangeMode += OnChangeCountToSpawn;
	}
	private void OnDisable () {
		Events.UI.OnChangeMode -= OnChangeCountToSpawn;
	}

	private void Initialize () {
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

	void OnChangeCountToSpawn(GameplayManager.Mode newMode) {
		choosenMode = newMode;
		Refresh ();
	}

	public void StartGame () {
		if (!AwailableToStart)
			return;
		else
			Events.Gameplay.OnStartGame.Invoke (choosenMode);
	}

	void Refresh () {
		startButton.ToggleTransition (AwailableToStart);
	}
}
