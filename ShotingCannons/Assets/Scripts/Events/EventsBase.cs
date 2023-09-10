using System;

public static class Events {
	static UIEvents _ui;
	static GameplayEvents _gameplay;
	static GamepadEvents _gamepad;
	public static UIEvents UI {
		get {
			if (_ui == null)
				_ui = new UIEvents ();
			return _ui;
		}
	}

	public static GameplayEvents Gameplay {
		get {
			if (_gameplay == null)
				_gameplay = new GameplayEvents ();
			return _gameplay;
		}
	}

	public static GamepadEvents Gamepad {
		get {
			if (_gamepad == null)
				_gamepad = new GamepadEvents ();
			return _gamepad;
		}
	}

	public abstract class EventBase {
		event Action _event;

		public EventBase () { }
		public void Subscribe (Action subscriber) {
			_event += subscriber;
		}
		public void Unsubscribe (Action subscriber) {
			_event -= subscriber;
		}
		public static EventBase operator + (EventBase e, Action subscriber) {
			e.Subscribe (subscriber);
			return e;
		}
		public static EventBase operator - (EventBase e, Action subscriber) {
			e.Unsubscribe (subscriber);
			return e;
		}

		protected void InvokeBase () {
			if (_event != null)
				_event.Invoke ();
		}
	}

	public class Event : EventBase {
		public Event () : base () { }
		public void Invoke () {
			InvokeBase ();
		}
		public static Event operator + (Event e, Action subscriber) {
			e.Subscribe (subscriber);
			return e;
		}
		public static Event operator - (Event e, Action subscriber) {
			e.Unsubscribe (subscriber);
			return e;
		}
	}
	public class Event<T> : EventBase {
		event Action<T> _event;

		public Event () : base () {
		}
		public void Subscribe (Action<T> subscriber) {
			_event += subscriber;
		}
		public void Unsubscribe (Action<T> subscriber) {
			_event -= subscriber;
		}

		public static Event<T> operator + (Event<T> e, Action<T> subscriber) {
			e.Subscribe (subscriber);
			return e;
		}
		public static Event<T> operator - (Event<T> e, Action<T> subscriber) {
			e.Unsubscribe (subscriber);
			return e;
		}

		public void Invoke (T args) {
			if (_event != null)
				_event.Invoke (args);
			
			InvokeBase ();
		}
	}
}
