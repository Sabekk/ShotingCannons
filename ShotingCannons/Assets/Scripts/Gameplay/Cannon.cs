using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
	[SerializeField] Bullet bullet;
	[SerializeField] GameObject body;
	[SerializeField] CircleCollider2D collider;
	[SerializeField] Transform rilfe;
	public float Radius => collider != null ? collider.radius : 0;
	public CircleCollider2D Collider => collider;
	public float Width => (transform as RectTransform).rect.width * currentScale;
	float rotationSpeed = 10;

	float rotationTime = 1;
	float shotingTime = 1;

	float nextRotation;

	float rotationTimer;
	float shotingTimer;

	float currentScale;
	Quaternion nextRotate = new Quaternion (0, 0, 0, 0);

	int lives = 1;
	bool recovering;
	float recoverTime = 2;
	float recorerTimer;

	private void Awake () {
		Events.Gameplay.OnCannonGetHit += OnGetHit;
	}
	private void OnDestroy () {
		Events.Gameplay.OnCannonGetHit -= OnGetHit;
	}
	private void Update () {
		if (!recovering) {
			rotationTimer += Time.deltaTime;
			shotingTimer += Time.deltaTime;
			if (rotationTimer >= nextRotation) {
				RotateCannon ();
			}
			if (shotingTimer >= shotingTime)
				Shot ();

			transform.rotation = Quaternion.Lerp (transform.rotation, nextRotate, rotationSpeed * Time.deltaTime);

		} else {
			recorerTimer += Time.deltaTime;
			if (recorerTimer >= recoverTime) {
				recorerTimer = 0;
				recovering = false;
				ChangeBodyStatus (true);
			}
		}
	}

	public void Initialzie (float scale) {
		transform.localScale = Vector3.one * scale;
		currentScale = scale;
		nextRotation = Random.Range (0.0f, rotationTime);
		CheckNextRotate ();
	}
	void RotateCannon () {
		rotationTimer = 0;
		nextRotation = Random.Range (0.0f, rotationTime);
		nextRotate = Quaternion.Euler (Vector3.forward * Random.Range (0.0f, 360.0f));
		CheckNextRotate ();
	}

	void CheckNextRotate () {
		if (nextRotate.x + nextRotate.y + nextRotate.z + nextRotate.w == 0)
			nextRotate = Quaternion.identity;
	}
	void Shot () {
		shotingTimer = 0;
		Bullet bullet = Instantiate (this.bullet, rilfe.position, rilfe.rotation);
		GameplayManager.Instance.MakeShotInBattlefield (bullet);
		bullet.Initialize (currentScale, gameObject);
	}
	void OnGetHit (GameObject hittedObject) {
		if (hittedObject != gameObject)
			return;
		if (recovering)
			return;
		if (lives <= 0)
			return;

		lives--;
		if (lives <= 0) {
			Destroy (gameObject);
			Events.Gameplay.OnCannonDestoyed.Invoke ();
		} else {
			ChangeBodyStatus (false);
			recovering = true;
		}
	}

	void ChangeBodyStatus (bool isEnable) {
		body.SetActive (isEnable);
		collider.enabled = isEnable;
	}

}
