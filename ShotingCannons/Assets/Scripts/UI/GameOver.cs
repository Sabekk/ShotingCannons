using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : ViewBase {
	public void ReturnToMainMenu () {
		Events.UI.OnReturnToMenu.Invoke ();
	}
}
