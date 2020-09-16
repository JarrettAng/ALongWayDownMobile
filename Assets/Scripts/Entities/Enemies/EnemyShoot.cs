using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Bullet bullet = default;
    [SerializeField] private Transform player = default;

    [Header("Shooting Attributes")]
    [SerializeField, Tooltip("Maximum y-distance from player before starting to shoot")] private float yMaxTolerance = 5f;
    [SerializeField, Tooltip("Maximum y-distance from player before starting to shoot")] private float yMinTolerance = -1f;
    [SerializeField, Tooltip("Maximum x-distance from player before starting to shoot")] private float xMinTolerance = 2f;

    [SerializeField, Tooltip("Time between shots")] private float cooldown = 3f;
    [SerializeField, Tooltip("Shooting height offset from ground")] private float yShootPosOffset = 0.8f;

    private EnemyController enemyController;
    private SoundManager soundManager;
    private Animator anim;

    private void Awake() {
        soundManager = SoundManager.Instance;

        anim = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    private void Start() {

        player = GameManager.Instance.Player.transform;

        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        StartCoroutine(HandleShooting());
    }

    private IEnumerator HandleShooting() {
        while(player.position.y > transform.position.y + yMinTolerance) {

            if(player.position.y < transform.position.y + yMaxTolerance) {
                if(player.position.x < transform.position.x - xMinTolerance && enemyController.facingLeft) {
                    ShootAtPlayer();
                    yield return new WaitForSeconds(cooldown);
                } else if(player.position.x > transform.position.x + xMinTolerance && !enemyController.facingLeft) {
                    ShootAtPlayer();
                    yield return new WaitForSeconds(cooldown);
                } else {
                    yield return new WaitForSeconds(1f);
                }
            } else {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void ShootAtPlayer() {
        Vector2 spawnPos = transform.position;
        spawnPos.y += yShootPosOffset;

        Bullet newBullet = Instantiate(bullet, spawnPos, Quaternion.identity);

        Vector2 playerPos = player.transform.position;
        playerPos.y += yShootPosOffset;

        soundManager.PlaySound("EnemyShoot");

        anim.SetTrigger("Shoot");

        newBullet.Setup(playerPos);
    }
}
