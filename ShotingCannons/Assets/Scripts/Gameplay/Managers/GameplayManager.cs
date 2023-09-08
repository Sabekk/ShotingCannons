using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoSingleton<GameplayManager> {
	public enum Mode { none, Cannons50, Cannons100, Cannons250, Cannons500 }
	[SerializeField] GameMode[] gameMods;
	[SerializeField] RectTransform battleField;

	GameMode currentMode;

	List<Vector2> possiblePositions;

	float widthfOfField;
	float heightfOfField;
	float widthOffset = 15;
	float heightOffset = 15;

	protected override void Awake () {
		base.Awake ();
		Events.Gameplay.OnStartGame += OnStartNewGame;
	}
	private void Start () {
		possiblePositions = new List<Vector2> ();
		widthfOfField = battleField.rect.width;
		heightfOfField = battleField.rect.height;
	}
	private void FixedUpdate () {

	}

	private void OnDestroy () {
		Events.Gameplay.OnStartGame -= OnStartNewGame;
	}
	void OnStartNewGame (Mode mode) {
		ChangeGameMode (mode);
		GeneratePossiblePositions (currentMode.cannonsCount);
		Cannon cannon = Resources.Load ("Cannon", typeof (Cannon)) as Cannon;
		cannon.Initialzie (currentMode.cannonScale);
		Vector2 spawnPoint = Vector2.zero;
		List<Vector2> neighbourPos = new List<Vector2> ();
		float space = cannon.Width;

		for (int i = 0; i < currentMode.cannonsCount; i++) {
			Cannon spawnedCannon = Instantiate (cannon, battleField);
			bool positionSet = false;

			while (!positionSet) {
				positionSet = true;

				if (possiblePositions.Count == 0) {
					Debug.LogError ("Missing positions");
					break;
				}

				int randomNumber = Random.Range (0, possiblePositions.Count);
				spawnPoint = possiblePositions[randomNumber];

				possiblePositions.RemoveAt (randomNumber);

				foreach (var pos in neighbourPos) {
					float distance = Vector2.Distance (pos, spawnPoint);
					if (distance < space) {
						positionSet = false;
						break;
					}
				}
				if (positionSet) {
					spawnedCannon.transform.localPosition = spawnPoint;
					neighbourPos.Add (spawnPoint);
				}
			}
		}
	}

	void ChangeGameMode (Mode mode) {
		foreach (var gameMode in gameMods)
			if (gameMode.mode == mode) {
				currentMode = gameMode;
				break;
			}
	}
	void GeneratePossiblePositions (int count) {
		possiblePositions.Clear ();
		float baseWidth = widthfOfField / (count / 2);
		float startPointInWidth = -(widthfOfField / 2);
		float baseHeight = heightfOfField / (count / 2);
		float startPointInHeight = -(heightfOfField / 2);
		for (int i = 1; i < count / 2; i++) {
			for (int j = 1; j < count / 2; j++) {
				float possibleX = startPointInWidth + baseWidth * i + Random.Range (0.5f, widthOffset);
				float possibleY = startPointInHeight + baseHeight * j + Random.Range (0.5f, heightOffset);
				possiblePositions.Add (new Vector2 (possibleX, possibleY));
			}
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
