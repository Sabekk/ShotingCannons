using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvents {
	public Events.Event<int> OnStartGame = new Events.Event<int> ();
	public Events.Event OnGameOver = new Events.Event ();
}
