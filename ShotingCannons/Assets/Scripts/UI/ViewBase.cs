using UnityEngine;

public abstract class ViewBase : MonoBehaviour {
	[SerializeField] GameObject view;
	public virtual void Activate () {
		view.SetActive (true);
	}
	public virtual void Deactivate () {
		view.SetActive (false);
	}
}
