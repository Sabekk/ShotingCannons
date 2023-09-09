using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
	[SerializeField] Bullet bullet;
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

	bool readyToShot;

	float currentScale = 1;
	Quaternion nextRotate;
	private void Awake () {
	}
	private void Update () {
		rotationTimer += Time.deltaTime;
		shotingTimer += Time.deltaTime;
		if (rotationTimer >= nextRotation) {
			RatateCannon ();
		}
		if (shotingTimer >= shotingTime)
			Shot ();

		transform.rotation = Quaternion.Lerp (transform.rotation, nextRotate, rotationSpeed * Time.deltaTime);
	}
	void RatateCannon () {
		rotationTimer = 0;
		nextRotation = Random.Range (0.0f, rotationTime);
		nextRotate = Quaternion.Euler (Vector3.forward * Random.Range (0.0f, 360.0f));
	}
	void Shot () {
		shotingTimer = 0;
		Bullet bullet = Instantiate (this.bullet, rilfe.position, rilfe.rotation);
		GameplayManager.Instance.MakeShotInBattlefield (bullet);
		bullet.Initialize (currentScale);
	}
	public void Initialzie (float scale) {
		transform.localScale = Vector3.one * scale;
		currentScale = scale;
		nextRotation = Random.Range (0.0f, rotationTime);
	}
}
