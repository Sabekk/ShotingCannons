using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoSingleton<GameplayManager> {
	public enum Mode { none, Cannons50, Cannons100, Cannons250, Cannons500 }
	[SerializeField] GameMode[] gameMods;
	
	GameMode currentMode;

	int objectsInGame = 0;

	protected override void Awake () {
		base.Awake ();
		Events.Gameplay.OnStartGame += OnStartNewGame;
		Events.Gameplay.OnCannonDestoyed += OnCannonDestroyed;
	}
	

	private void OnDestroy () {
		Events.Gameplay.OnStartGame -= OnStartNewGame;
		Events.Gameplay.OnCannonDestoyed -= OnCannonDestroyed;
	}
	void OnStartNewGame (Mode mode) {
		objectsInGame = GetModeCannonsCount (mode);
		ChangeGameMode (mode);
		Events.Gameplay.OnSpawnCannons.Invoke (currentMode.cannonsCount, currentMode.cannonScale);
	}
	void OnCannonDestroyed () {
		objectsInGame--;
		if (objectsInGame <= 1)
			Events.Gameplay.OnGameOver.Invoke ();
	}

	void ChangeGameMode (Mode mode) {
		foreach (var gameMode in gameMods)
			if (gameMode.mode == mode) {
				currentMode = gameMode;
				break;
			}
	}
	
	public int GetModeCannonsCount (Mode mode) {
		foreach (var gameMode in gameMods)
			if (gameMode.mode == mode) {
				return gameMode.cannonsCount;
			}
		return 0;
	}

	[System.Serializable]
	struct GameMode {
		public Mode mode;
		public float cannonScale;
		public int cannonsCount;
	}
}
