using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
	[SerializeField] CircleCollider2D collider;
	public float Radius => collider != null ? collider.radius : 0;
	public CircleCollider2D Collider => collider;
	public float Width => (transform as RectTransform).rect.width * currentScale;

	float currentScale = 1;
	private void Awake () {
	}
	public void Initialzie (float scale) {
		transform.localScale = Vector3.one * scale;
		currentScale = scale;
	}
}
