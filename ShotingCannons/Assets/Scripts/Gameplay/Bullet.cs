using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	Rigidbody2D rb;
	float speed = 250;
	private void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	private void Update () {
		rb.velocity = transform.up * speed;
	}
	public void Initialize (float scale) {
		transform.localScale = Vector3.one * scale;
	}
}
