using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBullet : MonoBehaviour
{
    [SerializeField] private float initialUpVelocity = 5f;
    [SerializeField, Tooltip("How far left or right can the bullet go")] private float xSprayAngle = 3f;

    private Rigidbody2D rb2d;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();

        float randomXAngle = Random.Range(-xSprayAngle, xSprayAngle);

        rb2d.velocity = new Vector2(randomXAngle, initialUpVelocity);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
