using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemyShoot : MonoBehaviour
{
    [Header("Shooting Attributes")]
    [SerializeField] private DeadBullet bullet = default;
    [SerializeField, Tooltip("Time between shots")] private float cooldown = 1f;
    [SerializeField, Tooltip("How long before the first shot takes?")] private float initialCooldown = 1f;

    private WaitForSeconds cooldownTime;

    private SoundManager soundManager;

    private void Awake() {
        soundManager = SoundManager.Instance;
    }

    private void Start() {
        StartCoroutine(HandleShooting());

        cooldownTime = new WaitForSeconds(cooldown);
    }

    private IEnumerator HandleShooting() {
        yield return new WaitForSeconds(initialCooldown);

        while(true) {
            ShootAtPlayer();
            yield return cooldownTime;
        }
    }

    private void ShootAtPlayer() {
        Vector2 spawnPos = transform.position;

        DeadBullet newBullet = Instantiate(bullet, spawnPos, Quaternion.identity);

        soundManager.PlaySound("EnemyShoot");
    }
}
