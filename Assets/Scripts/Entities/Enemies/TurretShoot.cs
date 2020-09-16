using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    [SerializeField] private Bullet bullet = default;
    [SerializeField] private Transform player = default;

    [Header("Shooting Attributes")]
    [SerializeField, Tooltip("Maximum y-distance from player before starting to shoot")] private float yMaxTolerance = 5f;
    [SerializeField, Tooltip("Maximum y-distance from player before starting to shoot")] private float yMinTolerance = -13f;
    // [SerializeField, Tooltip("Maximum x-distance from player before starting to shoot")] private float xMinTolerance = 2f;

    [SerializeField, Tooltip("Time between shots")] private float cooldown = 3f;
    [SerializeField, Tooltip("Time between checks if player in range")] private float checkCooldown = 1.2f;

    [SerializeField, Tooltip("Shooting height offset from ground")] private float yShootPosOffset = 0.8f;

    private WaitForSeconds checkTime;
    private WaitForSeconds shootCooldown;
    private WaitForEndOfFrame frame;

    private SoundManager soundManager;

    private void Awake() {
        soundManager = SoundManager.Instance;

        checkTime = new WaitForSeconds(checkCooldown);
        shootCooldown = new WaitForSeconds(cooldown);
        frame = new WaitForEndOfFrame();
    }

    private void Start() {
        player = GameManager.Instance.Player.transform;

        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        StartCoroutine(WaitForPlayerInRange());
    }

    private IEnumerator WaitForPlayerInRange() {
        while(player.position.y > transform.position.y + yMaxTolerance) {
            yield return checkTime;
        }

        StartCoroutine(HandleShooting());
        StartCoroutine(LookAtPlayer());
    }

    private IEnumerator HandleShooting() {
        while(player.position.y > transform.position.y + yMinTolerance) {
            ShootAtPlayer();

            yield return shootCooldown;
        }
    }

    private IEnumerator LookAtPlayer() {
        while(player.position.y > transform.position.y + yMinTolerance) {
            transform.up = player.transform.position - transform.position;

            yield return frame;
        }
    }

    private void ShootAtPlayer() {
        Vector2 spawnPos = transform.position;

        Bullet newBullet = Instantiate(bullet, spawnPos, Quaternion.identity);

        Vector2 playerPos = player.transform.position;
        playerPos.y += yShootPosOffset;

        soundManager.PlaySound("EnemyShoot");

        newBullet.Setup(playerPos);
    }
}
