using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public void SetHorizontalDirection(float dir) {
        EventManager.HorizontalInput.Invoke(dir);
    }

    public void SetJumpDirection(float dir) {
        EventManager.JumpInput.Invoke(dir);
    }

    public void SetFallDirection(float dir) {
        EventManager.FallInput.Invoke(dir);
    }
}
