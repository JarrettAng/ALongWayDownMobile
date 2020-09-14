using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnPoint : MonoBehaviour
{
    [SerializeField, Tooltip("Should the enemy turn left or right on contact?")] private bool turnLeft = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<EnemyController>().SwapDirection(turnLeft);
        }
    }
}
