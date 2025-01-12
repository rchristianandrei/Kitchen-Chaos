using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        this.playerInputActions = new PlayerInputActions();
        this.playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 input = this.playerInputActions.Player.Move.ReadValue<Vector2>();
        input = input.normalized;

        return input;
    }
}
