using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : ViewBase {
	[SerializeField] ModeButton[] buttons;
	[SerializeField] StartButton startButton;

	int choosenCountToSpawn = 0;
	public bool AwailableToStart => choosenCountToSpawn > 0;
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

	void OnChangeCountToSpawn(int count) {
		choosenCountToSpawn = count;
		Refresh ();
	}

	public void StartGame () {
		if (!AwailableToStart)
			return;
		else
			Events.Gameplay.OnStartGame.Invoke (choosenCountToSpawn);
	}

	void Refresh () {
		startButton.ToggleTransition (choosenCountToSpawn > 0);
	}
}
