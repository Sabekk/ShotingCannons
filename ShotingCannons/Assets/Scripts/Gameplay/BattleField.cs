using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour
{
	[SerializeField] RectTransform battleField;
	List<Vector2> possiblePositions;

	float widthfOfField;
	float heightfOfField;
	float widthOffset = 15;
	float heightOffset = 15;

	private void Awake () {
		Events.Gameplay.OnSpawnCannons += OnSpawnCannons;
		Events.Gameplay.OnGameOver += OnGameOver;
		Events.Gameplay.OnBulletShoted += OnBulletShoted;
	}
	private void OnDestroy () {
		Events.Gameplay.OnSpawnCannons -= OnSpawnCannons;
		Events.Gameplay.OnGameOver -= OnGameOver;
		Events.Gameplay.OnBulletShoted -= OnBulletShoted;
		possiblePositions.Clear ();
	}
	private void Start () {
		possiblePositions = new List<Vector2> ();
		widthfOfField = battleField.rect.width;
		heightfOfField = battleField.rect.height;
	}

	void OnSpawnCannons (int count, float scale) {
		battleField.gameObject.SetActive (true);

		GeneratePossiblePositions (count);
		Cannon cannon = Resources.Load ("Cannon", typeof (Cannon)) as Cannon;
		Vector2 spawnPoint = Vector2.zero;
		List<Vector2> neighbourPos = new List<Vector2> ();

		for (int i = 0; i < count; i++) {
			Cannon spawnedCannon = Instantiate (cannon, battleField);
			spawnedCannon.Initialzie (scale);

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

	void OnGameOver () {
		for (int i = battleField.childCount - 1; i >= 0; i--) {
			Destroy (battleField.GetChild (i).gameObject);
		}
		battleField.gameObject.SetActive (false);
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

	public void OnBulletShoted (Bullet bullet) {
		bullet.gameObject.transform.SetParent (battleField);
	}
}
