using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Movement attributes")]
	[SerializeField, Tooltip("How fast can the player move?")] private float moveSpeed = 5f;
	[SerializeField, Tooltip("How high can the player jump?")] private float jumpHeight = 6f;
	[SerializeField, Tooltip("Affects the falling speed of the player over time.")] private float fallMultiplier = 2f;
	[SerializeField, Tooltip("How much should the falling multiplier increase by on down press")] private float downMultiplier = 3f;

	[Header("Interaction attributes")]
	[SerializeField, Tooltip("How much knockback to apply to the player when hit.")] private Vector2 knockbackForce = default;
	[SerializeField, Tooltip("How long till the player can control his character after knockback.")] private float knockbackDuration = 1f;

	[Header("Controllers reference")]
	[SerializeField] private ParticleController particleController = default;

	[Header("Read-Only")]
	[SerializeField] private float horizontalDirection;
	[SerializeField] private float jumpDirection;
	[SerializeField] private float fallDirection;

	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer rend;

    private SoundManager soundManager;

	private bool canJump = false;
	private bool facingLeft = true;
	private bool canControl = true;

    private float gravity;

	// In charge of moving the player (left, right, jump)

	private void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		rend = GetComponent<SpriteRenderer>();

        soundManager = SoundManager.Instance;

        gravity = Physics2D.gravity.y;

        EventManager.OnPlayerHit += HandlePlayerHit;
		EventManager.HorizontalInput.AddListener(SetHorizontalDirection);
		EventManager.JumpInput.AddListener(SetJumpDirection);
		EventManager.FallInput.AddListener(SetFallDirection);
	}

	private void Update() {
		HandleRealisticFall();
        HandleJump();

        if(!canControl) {
			return;
		}

        HandleMovement();
	}

	public void SetHorizontalDirection(float dir) {
		horizontalDirection = dir;
    }

	public void SetJumpDirection(float dir) {
		jumpDirection = dir;
	}

	public void SetFallDirection(float dir) {
		fallDirection = dir;
	}

	private void HandleJump() {
		if(jumpDirection > 0 && canJump) {
			// The player's y velocity should reset on jump (so jump height is constant no matter the current velocity)
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);

			// Player should only be able to jump once (until he touches a platform)
			canJump = false;

            soundManager.PlaySound("PlayerJump");

            anim.SetBool("InAir", true);
		}
	}

	private void HandleMovement() {
		float direction = horizontalDirection;
		rb2d.velocity = new Vector2(direction * moveSpeed, rb2d.velocity.y);

		if(direction < 0) {
			if(!facingLeft) {
				transform.localScale = new Vector2(1, 1);
				facingLeft = true;
			}
		} else if(direction > 0) {
			if(facingLeft) {
				transform.localScale = new Vector2(-1, 1);
				facingLeft = false;
			}
		}

		if(direction == 0) {
			anim.SetBool("IsMoving", false);
		} else {
			anim.SetBool("IsMoving", true);
		}

		if(rb2d.velocity.x != 0 || rb2d.velocity.y != 0) {
			particleController.ToggleEmitOnThisParticle(ParticleType.PLAYER_WALK, true);
		} else {
			particleController.ToggleEmitOnThisParticle(ParticleType.PLAYER_WALK, false);
		}
	}

	private void HandleRealisticFall() {
        // If player is falling, make player fall faster over time.
        if(fallDirection > 0) {
            rb2d.velocity += Vector2.up * gravity * (fallMultiplier * downMultiplier - 1) * Time.deltaTime;
        } else if(rb2d.velocity.y < 0) {
            rb2d.velocity += Vector2.up * gravity * (fallMultiplier - 1) * Time.deltaTime;

        }
	}

	private void HandlePlayerHit(GameObject hitObject) {
		StartCoroutine(TriggerMovementCooldown());
		rb2d.velocity = Vector2.zero;

		if(hitObject.transform.position.x > transform.position.x) {
			rb2d.AddForce(new Vector2(-knockbackForce.x, knockbackForce.y), ForceMode2D.Impulse);
		} else {
			rb2d.AddForce(knockbackForce, ForceMode2D.Impulse);
		}
	}

	private IEnumerator TriggerMovementCooldown() {
		canControl = false;
        yield return new WaitForSeconds(knockbackDuration);
        canControl = true;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		// Player hiting the ceiling of anything above him doesn't count!
		if(other.transform.position.y > transform.position.y) {
			return;
		}

		anim.SetBool("InAir", false);
		canJump = true;

		// Make player controllable when touches platform if he isnt.
		if(!canControl) {
			canControl = !canControl;
		}
	}
}
