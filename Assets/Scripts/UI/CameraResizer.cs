using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    private void Awake() {
        BoxCollider2D screenWidthMeasurer = GetComponent<BoxCollider2D>();

        float expectedWidth = screenWidthMeasurer.bounds.size.x;

        Camera.main.orthographicSize = expectedWidth / (2.0f * Screen.width / Screen.height);
    }
}
