using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "Input Bindings";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnPauseAction;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternative,
        Pause,
    }
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Game Input should be singleton");
        }

        Instance = this;

        this.playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)) playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));

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

    public string GetBindingText(Binding binding)
    {
        return binding switch
        {
            Binding.Move_Up => playerInputActions.Player.Move.bindings[1].ToDisplayString(),
            Binding.Move_Down => playerInputActions.Player.Move.bindings[2].ToDisplayString(),
            Binding.Move_Left => playerInputActions.Player.Move.bindings[3].ToDisplayString(),
            Binding.Move_Right => playerInputActions.Player.Move.bindings[4].ToDisplayString(),
            Binding.Interact => playerInputActions.Player.Interact.bindings[0].ToDisplayString(),
            Binding.InteractAlternative => playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString(),
            Binding.Pause => playerInputActions.Player.Pause.bindings[0].ToDisplayString(),
            _ => string.Empty,
        };
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindIndex = 0;

        switch (binding)
        {
            default:

            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindIndex = 1;
                break;

            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindIndex = 2;
                break;

            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindIndex = 3;
                break;

            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindIndex = 4;
                break;

            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                break;

            case Binding.InteractAlternative:
                inputAction = playerInputActions.Player.InteractAlternate;
                break;

            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                break;    
        }

        inputAction.PerformInteractiveRebinding(bindIndex).OnComplete(callback =>
        {
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
        }).Start();
    }
}
