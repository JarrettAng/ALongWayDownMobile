using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	[Header("Movement Attributes")]
	[SerializeField] private float moveSpeed = 1.5f;

    public bool facingLeft = false;

    private Rigidbody2D rb2d;
	private SpriteRenderer rend;


	private void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();

		bool startLeft = Random.value > 0.5f;
		SwapDirection(startLeft);
	}

	private void Update() {
		if(rb2d.velocity.x == -moveSpeed) {
			if(!facingLeft) {
				rend.flipX = true;
				facingLeft = true;
			}
		} else if(rb2d.velocity.x == moveSpeed) {
			if(facingLeft) {
				rend.flipX = false;
				facingLeft = false;
			}
		} else {
			if(facingLeft) {
				rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
			} else {
				rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
			}
		}
	}

	public void SwapDirection(bool turnLeft) {
		if(turnLeft) {
			rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
		} else {
			rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
		}
	}
}
