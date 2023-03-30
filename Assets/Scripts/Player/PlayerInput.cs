using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent OnJumpAction;

    public UnityEvent OnSprintPerformed;
    public UnityEvent OnSprintCanceled;

    public UnityEvent OnPauseButton;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Jump.performed += Jump_performed;

        playerInputActions.Player.Sprint.performed += Sprint_performed;
        playerInputActions.Player.Sprint.canceled += Sprint_canceled;

        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseButton?.Invoke();
    }

    private void Sprint_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSprintCanceled?.Invoke();
    }

    private void Sprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSprintPerformed?.Invoke();
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
