using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField, Tooltip("How fast should the camera snap to the player's position?")] private float followSharpness = 0.1f;
    [SerializeField] private float yOffset = -5f;

    [Header("Read-Only")]
    [SerializeField, Tooltip("The player for the camera to follow")] private Transform player;
    //[SerializeField] private float leftExtent;
    //[SerializeField] private float rightExtent;
    [SerializeField] private Vector3 newCamPos;

    private void Awake() {
        SetupCameraExtents();

        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.LogWarning("Player object for camera not set!");
        }

        void SetupCameraExtents() {
            //float horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

            //leftExtent = Camera.main.transform.position.x - horizontalExtent / 2;
            //rightExtent = Camera.main.transform.position.x + horizontalExtent / 2;

            // Setup the z position of the camera (if we don't the camera's z will be 0 and the player won't see anything)
            newCamPos = transform.position;
        }
    }

    private void LateUpdate() {
        float blend = 1f - Mathf.Pow(1f - followSharpness, Time.deltaTime * 30f);

        // newCamPos.x = Mathf.Clamp(player.position.x, leftExtent, rightExtent);
        newCamPos.y = player.position.y + yOffset;

        transform.position = Vector3.Lerp(transform.position, newCamPos, blend);
    }
}
