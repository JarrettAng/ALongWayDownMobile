using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsJoystick : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Joystick movementJoystick = default;
    [SerializeField] private Joystick actionJoystick = default;

    [Header("Read-Only")]
    [SerializeField] private float currentXAxis;
    [SerializeField] private float currentYAxis;

    private Controls controls;

    private void Awake() {
        controls = GetComponent<Controls>();
    }

    private void Update() {
        HandleHorizontalInput();
        HandleVerticalInput();
    }

    private void HandleHorizontalInput() {
        if(currentXAxis == movementJoystick.Horizontal) return;

        currentXAxis = movementJoystick.Horizontal;
        controls.SetHorizontalDirection(currentXAxis);
    }


    private void HandleVerticalInput() {
        if(currentYAxis == actionJoystick.Vertical) return;

        currentYAxis = actionJoystick.Vertical;

        switch(currentYAxis) {
            case 1:
                controls.SetJumpDirection(1);
                controls.SetFallDirection(0);
                break;

            case 0:
                controls.SetJumpDirection(0);
                controls.SetFallDirection(0);
                break;

            case -1:
                controls.SetJumpDirection(0);
                controls.SetFallDirection(1);
                break;

            default:
                Debug.LogErrorFormat("Action (Jump & Fall) Joystick has invalid input of {0}, expected range (-1 to 1)", currentYAxis);
                break;
        }
    }
}
