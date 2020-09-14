using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyHandler : MonoBehaviour {

	[SerializeField, Tooltip("How much bounce does the player get when jumping on this enemy.")] private float bounceAmount;
	[SerializeField, Tooltip("Enemy that spawns on death")] private GameObject deadEnemy;

	[SerializeField] private ScorePopUp addScorePopUp;

    private GameManager gameManager;

    private void Awake() {
        gameManager = GameManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
		GameObject collidedObject = collision.collider.gameObject;

		// If the colliding object is not a player, E.g other enemies, ignore it.)
		if(!collidedObject.CompareTag("Player")) {
			return;
		}

        collidedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounceAmount), ForceMode2D.Impulse);

		HandleDestroy();
	}

    private void HandleDestroy() {
        EventManager.OnPlayerScore?.Invoke(gameManager.EnemyScoreIncrease);
        EventManager.OnEnemyKill?.Invoke();

        CameraShake.Instance.TriggerShake();

        GameObject dead = Instantiate(deadEnemy, transform.parent.position, Quaternion.identity);
        dead.transform.parent = transform.parent.parent;

        ScorePopUp popUp = Instantiate(addScorePopUp, transform.position, Quaternion.identity);
        popUp.InitializePopUp(gameManager.EnemyScoreIncrease);

        SoundManager.Instance.PlaySound("EnemyDeath");

        Destroy(transform.parent.gameObject);
    }
}
