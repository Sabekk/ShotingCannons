using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	[SerializeField] MainMenu mainMenu;
	[SerializeField] GameOver gameOverView;

	private void Awake () {
		Events.Gameplay.OnStartGame += OnStartNewGame;
		Events.Gameplay.OnGameOver += OnGameOver;
		Events.UI.OnReturnToMenu += OnReturnToMenu;
	}

	void OnStartNewGame (int count) {
		mainMenu.Deactivate ();
	}
	void OnGameOver () {
		gameOverView.Activate ();
	}

	void OnReturnToMenu () {
		gameOverView.Deactivate ();
		mainMenu.Activate ();
	}
}
