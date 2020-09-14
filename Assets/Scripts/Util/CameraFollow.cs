using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField, Tooltip("The player for the camera to follow")] private Transform player;

    [SerializeField, Tooltip("How fast should the camera snap to the player's position?")] private float followSharpness = 0.1f;

    private Vector3 newCamPos;

    private void Awake() {
        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.LogWarning("Player object for camera not set!");
        }
    }

    private void LateUpdate() {
        float blend = 1f - Mathf.Pow(1f - followSharpness, Time.deltaTime * 30f);

        // We only want to follow the player's y position
        newCamPos = transform.position;
        newCamPos.y = player.position.y;

        transform.position = Vector3.Lerp(transform.position, newCamPos, blend);
    }
}
