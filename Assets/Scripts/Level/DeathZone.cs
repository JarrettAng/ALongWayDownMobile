using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField, Tooltip("The object the deathzone should follow")] private Transform player;
    [SerializeField, Tooltip("How much higher should the deathzone be?")] private float yOffset = 26f;

    private Vector2 newPos;

    private void Awake() {
        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.LogWarning("Player object for deathzone not set!");
        }

        newPos = player.position;
        newPos.y += yOffset;
        transform.position = newPos;
    }

    private void LateUpdate() {
        newPos.y = player.position.y + yOffset;
        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }
}
