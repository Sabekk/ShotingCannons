using UnityEngine;

public class GameplayEvents {
	public Events.Event<GameplayManager.Mode> OnStartGame = new Events.Event<GameplayManager.Mode> ();
	public Events.Event<int, float> OnSpawnCannons = new Events.Event<int, float> ();
	public Events.Event OnGameOver = new Events.Event ();
	public Events.Event<GameObject> OnCannonGetHit = new Events.Event<GameObject> ();
	public Events.Event OnCannonDestoyed = new Events.Event ();
	public Events.Event<Bullet> OnBulletShoted = new Events.Event<Bullet> ();
}
