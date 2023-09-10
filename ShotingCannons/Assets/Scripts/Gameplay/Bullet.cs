using UnityEngine;

public class Bullet : MonoBehaviour {
	Rigidbody2D rb;
	GameObject owner;
	float speed = 250;
	private void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	private void Update () {
		rb.velocity = transform.up * speed;
		if (IsOutOfScreen ())
			Destroy (gameObject);
	}

	private void OnTriggerEnter2D (Collider2D collision) {
		if (collision.gameObject.tag == "Cannon") {
			if (collision.gameObject == owner)
				return;
			Events.Gameplay.OnCannonGetHit.Invoke (collision.gameObject);
			Destroy (gameObject);
		}
	}

	public void Initialize (float scale, GameObject owner) {
		transform.localScale = Vector3.one * scale;
		this.owner = owner;
	}
	bool IsOutOfScreen () {
		return !Screen.safeArea.Contains (transform.position);
	}
}
