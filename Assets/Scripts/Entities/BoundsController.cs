using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsController : MonoBehaviour {
	private static float leftExtent = 0;
	private static float rightExtent = 0;

	[SerializeField, Tooltip("How far in should the player move to center on teleport? (Prevents infinite teleporting)")] private float teleportOffset = 0.1f;

	private void Awake() {
		if(leftExtent == 0) {
			float horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
			leftExtent = Camera.main.transform.position.x - horizontalExtent;
		}
		if(rightExtent == 0) {
			float horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
			rightExtent = Camera.main.transform.position.x + horizontalExtent;
		}
	}

	private void Update() {
		if(!IsWithinBounds()) {
			Teleport();
		}
	}

	private bool IsWithinBounds() {
		bool withinBounds;

		if(transform.position.x > leftExtent && transform.position.x < rightExtent) {
			withinBounds = true;
		} else {
			withinBounds = false;
		}

		return withinBounds;
	}

	/// <summary>
	/// Teleports the player to the left or right of screen depending on current position
	/// </summary>
	private void Teleport() {
		EventManager.OnObjectTeleport?.Invoke(gameObject);

		if(transform.position.x < 0) {
			transform.position = new Vector2(rightExtent - teleportOffset, transform.position.y);
		} else {
			transform.position = new Vector2(leftExtent + teleportOffset, transform.position.y);
		}
	}
}
