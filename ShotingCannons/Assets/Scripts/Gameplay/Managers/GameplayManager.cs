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

	int objectsInGame = 0;

	protected override void Awake () {
		base.Awake ();
		Events.Gameplay.OnStartGame += OnStartNewGame;
		Events.Gameplay.OnCannonDestoyed += OnCannonDestroyed;
		Events.Gameplay.OnGameOver += OnGameOver;
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
		Events.Gameplay.OnCannonDestoyed -= OnCannonDestroyed;
		Events.Gameplay.OnGameOver -= OnGameOver;
	}
	void OnStartNewGame (Mode mode) {
		battleField.gameObject.SetActive (true);

		objectsInGame = 0;
		ChangeGameMode (mode);
		GeneratePossiblePositions (currentMode.cannonsCount);
		Cannon cannon = Resources.Load ("Cannon", typeof (Cannon)) as Cannon;
		Vector2 spawnPoint = Vector2.zero;
		List<Vector2> neighbourPos = new List<Vector2> ();

		for (int i = 0; i < currentMode.cannonsCount; i++) {
			Cannon spawnedCannon = Instantiate (cannon, battleField);
			spawnedCannon.Initialzie (currentMode.cannonScale);
			objectsInGame++;

			float space = spawnedCannon.Width;
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
	void OnCannonDestroyed () {
		objectsInGame--;
		if (objectsInGame <= 1)
			Events.Gameplay.OnGameOver.Invoke ();
	}

	void OnGameOver () {
		for (int i = battleField.childCount - 1; i >= 0; i--) {
			Destroy (battleField.GetChild (i).gameObject);
		}
		battleField.gameObject.SetActive (false);
	}

	void ChangeGameMode (Mode mode) {
		foreach (var gameMode in gameMods)
			if (gameMode.mode == mode) {
				currentMode = gameMode;
				break;
			}
	}
	void GeneratePossiblePositions (int count) {
		if (count < 50)
			count = 50;
		int countToFind = count / 2;
		possiblePositions.Clear ();
		float baseWidth = widthfOfField / (countToFind);
		float startPointInWidth = -(widthfOfField / 2);
		float baseHeight = heightfOfField / (countToFind);
		float startPointInHeight = -(heightfOfField / 2);
		for (int i = 1; i < countToFind; i++) {
			for (int j = 1; j < countToFind; j++) {
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
	public void MakeShotInBattlefield (Bullet bullet) {
		bullet.gameObject.transform.SetParent (battleField);
	}

	[System.Serializable]
	struct GameMode {
		public Mode mode;
		public float cannonScale;
		public int cannonsCount;
	}
}
