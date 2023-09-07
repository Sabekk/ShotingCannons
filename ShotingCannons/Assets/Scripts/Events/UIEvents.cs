using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents {
	public Events.Event<int> OnChangeMode = new Events.Event<int> ();
	public Events.Event OnReturnToMenu = new Events.Event ();
}
