using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealth : MonoBehaviour {
	[Header("Player's information")]
	[SerializeField, Tooltip("Max HP of player")] private int maxHealth = 3;
	[Tooltip("Current HP of player")] private int health = 3;

    [SerializeField] private float hitGraceSeconds = 3f;

	[Header("Controlers reference")]
	[SerializeField] private ParticleController particleController;

	public PostProcessVolume postfx;

	private SoundManager soundManager;

    private Animator anim;

    private bool hitRecently;

	private void Awake() {
		EventManager.OnObjectTeleport += StopWalkParticle;

		soundManager = SoundManager.Instance;
        anim = GetComponent<Animator>();
	}

	private void Start() {
		health = maxHealth;
		HealthUI.Instance.UpdateUIHeart(health);
	}

	private void StopWalkParticle(GameObject teleportedObj) {
		if(teleportedObj == gameObject) {
			particleController.ToggleEmitOnThisParticle(ParticleType.PLAYER_WALK, false);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		GameObject collidedObject = collision.collider.gameObject;

		// If player collided with an enemy and is not jumping on it, hurt the player.
		// Only triggers if the lowest point of the player is below the top of the enemy.
		if(collision.collider.CompareTag("Enemy")) {
			PlayerHit();
			EventManager.OnPlayerHit?.Invoke(collidedObject);
		}
	}

    private void PlayerHit() {
        CameraShake.Instance.TriggerShake();

        particleController.InstantiateParticleOfType(ParticleType.PLAYER_HIT, transform.position);

        soundManager.PlaySound("PlayerHit");

        StartCoroutine(HandleHitPostFX());

        HandleHealth();

        void HandleHealth() {
            if(!hitRecently) {
                hitRecently = true;
                Invoke("ResetHitGracePeriod", hitGraceSeconds);

                anim.SetTrigger("Hit");

            } else { // If hit recently, give damage grace so they don't get comboed
                return;
            }

            health--;
            HealthUI.Instance.UpdateUIHeart(health);

            if(health <= 0) {
                EventManager.OnGameOver?.Invoke();
            }
        }
    }

    private void ResetHitGracePeriod() {
        hitRecently = false;
    }

    private IEnumerator HandleHitPostFX() {
        postfx.sharedProfile.TryGetSettings(out ChromaticAberration ca);

        float progress = .25f;

        float temp;
        temp = ca.intensity.value;
        while(progress < 1f) {

            ca.intensity.value = progress;

            progress += Time.deltaTime * 3;

            yield return new WaitForEndOfFrame();
        }


        float fprogress = 1f;

        float ftemp;
        ftemp = ca.intensity.value;
        while(fprogress > .25f) {

            ca.intensity.value = progress;

            progress -= Time.deltaTime * 3;

            yield return new WaitForEndOfFrame();
        }

        ftemp = .25f;
        ca.intensity.value = ftemp;

        yield return null;
    }
}
