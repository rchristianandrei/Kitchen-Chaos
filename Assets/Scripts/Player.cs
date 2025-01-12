using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private LayerMask countersLayerMask;

    private Vector3 lastInteractDir;

    public bool IsWalking { get; private set; }

    private void Start()
    {
        this.gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        this.Interact();
    }

    private void Update()
    {
        this.FindLastInteractDirection();
        this.Movement();
        this.Rotation();
    }

    private void FindLastInteractDirection()
    {
        Vector2 input = this.gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(input.x, 0, input.y);

        if (moveDir != Vector3.zero) this.lastInteractDir = moveDir;
    }

    private void Interact()
    {
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, this.lastInteractDir, out RaycastHit hitInfo, interactDistance, this.countersLayerMask))
        {
            if(hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void Movement()
    {
        Vector2 input = this.gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(input.x, 0, input.y);

        float moveDistance = this.movementSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = 0.7f;

        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        this.IsWalking = moveDir != Vector3.zero;

        if (!canMove)
        {
            // Check X
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Check Y
                var moveDirY = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirY, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirY;
                }
            }
        }

        if(canMove)
            transform.position += moveDistance * moveDir;
    }

    private void Rotation()
    {
        Vector2 input = this.gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(input.x, 0, input.y);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, this.rotationSpeed * Time.deltaTime);
    }
}
