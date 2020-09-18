using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTriangles : MonoBehaviour
{
    private void Awake() {
        MoveIntoPosition();

        GetComponent<ParticleSystem>().Play();

        void MoveIntoPosition() {
            Vector2 bottomOfScreen = transform.position;

            bottomOfScreen.y = -Camera.main.orthographicSize;

            transform.position = bottomOfScreen;
        }
    }
}
