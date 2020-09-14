using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField] private float moveSpeed = 4f;

	[SerializeField] private ParticleController particleController;

	private Rigidbody2D rb2d;

	private void Awake() {
		EventManager.OnObjectTeleport += StopBulletTrail;
		//EventManager.OnObjectFinishTeleport += StartBulletTrail;
	}

	private void Update() {
		if(rb2d.velocity.x != 0 || rb2d.velocity.y != 0) {
			particleController.ToggleEmitOnThisParticle(ParticleType.ENEMY_BULLET, true);
		}
	}

	private void StartBulletTrail(GameObject teleportedObj) {
		if(teleportedObj == gameObject) {
			particleController.ToggleEmitOnThisParticle(ParticleType.ENEMY_BULLET, true);
		}
	}

	private void StopBulletTrail(GameObject teleportedObj) {
		if(teleportedObj == gameObject) {
			particleController.ToggleEmitOnThisParticle(ParticleType.ENEMY_BULLET, false);
		}
	}

	public void Setup(Vector3 playerPos) {
		rb2d = GetComponent<Rigidbody2D>();

		Vector2 direction = transform.position - playerPos;
		transform.up = direction;

		rb2d.velocity = transform.up * -moveSpeed;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player")) {
			Destroy(gameObject);
		}
	}

	private void OnDisable() {
		EventManager.OnObjectTeleport -= StopBulletTrail;
		//EventManager.OnObjectFinishTeleport -= StartBulletTrail;
	}
}
