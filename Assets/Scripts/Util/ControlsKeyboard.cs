using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsKeyboard : MonoBehaviour
{
    [Header("Movement controls")] // Used to prevent hardcoding of keys (Use ProjectSettings > Input to change/set more keys of type)
    [SerializeField, Tooltip("Axes for left / right movement")] private string horizontalMovement = "Horizontal";
    [SerializeField, Tooltip("Axes for jump movement")] private string jumpMovement = "Jump";
    [SerializeField, Tooltip("Axes for fall movement")] private string fallMovement = "Down";

    private void Update() {
        if(Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0) return;

        HandleHorizontalInput();
        HandleJumpInput();
        HandleFallInput();
    }

    private void HandleHorizontalInput() {
        float dir = Input.GetAxisRaw(horizontalMovement);

        EventManager.HorizontalInput.Invoke(dir);
    }

    private void HandleJumpInput() {
        float dir = Input.GetAxisRaw(jumpMovement);

        EventManager.JumpInput.Invoke(dir);
    }

    private void HandleFallInput() {
        float dir = Input.GetAxisRaw(fallMovement);

        EventManager.FallInput.Invoke(dir);
    }
}
