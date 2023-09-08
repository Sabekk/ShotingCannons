using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents {
	public Events.Event<GameplayManager.Mode> OnChangeMode = new Events.Event<GameplayManager.Mode> ();
	public Events.Event OnReturnToMenu = new Events.Event ();
}
