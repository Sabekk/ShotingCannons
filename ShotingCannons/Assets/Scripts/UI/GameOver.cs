public class GameOver : ViewBase {
	public void ReturnToMainMenu () {
		Events.UI.OnReturnToMenu.Invoke ();
	}

	public override void OnDpadAction (string type) {
		base.OnDpadAction (type);
		if (type == Dpad_DOWN)
			ReturnToMainMenu ();
	}
}
