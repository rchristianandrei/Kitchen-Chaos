using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Game Input should be singleton");
        }

        Instance = this;

        this.playerInputActions = new PlayerInputActions();
        this.playerInputActions.Player.Enable();

        this.playerInputActions.Player.Interact.performed += Interact_performed;
        this.playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;

        this.playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        this.playerInputActions.Dispose();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        this.OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        this.OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        this.OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 input = this.playerInputActions.Player.Move.ReadValue<Vector2>();
        input = input.normalized;

        return input;
    }
}
