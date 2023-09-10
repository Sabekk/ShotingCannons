using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvents {
	public Events.Event<GameplayManager.Mode> OnStartGame = new Events.Event<GameplayManager.Mode> ();
	public Events.Event OnGameOver = new Events.Event ();
	public Events.Event<GameObject> OnCannonGetHit = new Events.Event<GameObject> ();
	public Events.Event OnCannonDestoyed = new Events.Event ();
}
