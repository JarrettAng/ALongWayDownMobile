using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
	public static Action OnPlatformPass;
	public static Action<GameObject> OnPlayerHit;
    public static Action<int> OnPlayerScore;

    public static Action OnEnemyKill;

    public static Action<GameObject> OnObjectTeleport;

	public static Action OnGameOver;

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
