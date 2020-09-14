using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {
	public class UnityFloatEvent : UnityEvent<float> { };

	public static Action OnPlatformPass;
	public static Action<GameObject> OnPlayerHit;
    public static Action<int> OnPlayerScore;

    public static Action OnEnemyKill;

    public static Action<GameObject> OnObjectTeleport;

	public static Action OnGameOver;

	// UI Control Events
	public static UnityFloatEvent HorizontalInput = new UnityFloatEvent();
	public static UnityFloatEvent JumpInput = new UnityFloatEvent();
	public static UnityFloatEvent FallInput = new UnityFloatEvent();

	private void OnDisable() {
		ClearAllEvents();
	}

	public static void ClearAllEvents() {
		OnPlatformPass = null;
		OnPlayerHit = null;
        OnPlayerScore = null;

        OnEnemyKill = null;

        OnObjectTeleport = null;

		OnGameOver = null;
	}
}
