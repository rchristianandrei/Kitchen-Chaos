using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private GameInput gameInput;

    float playerHeight = 2f;
    float playerRadius = 0.7f;

    public bool IsWalking { get; private set; }

    private void Update()
    {
        this.Movement();
    }

    private void Movement()
    {
        Vector2 input = this.gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(input.x, 0, input.y);

        float moveDistance = this.movementSpeed * Time.deltaTime;
        
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        this.IsWalking = moveDir != Vector3.zero;


        if (!canMove)
        {
            // Check X
            var moveDirX = this.CanMoveOnDir(new Vector3(moveDir.x, 0, 0).normalized, moveDistance);

            if (moveDirX == Vector3.zero)
            {
                var moveDirY =this.CanMoveOnDir(new Vector3 (0, 0, moveDir.z).normalized, moveDistance);

                if (moveDirY == Vector3.zero)
                {
                    canMove = false;
                }
                else
                {
                    moveDir = moveDirY;
                    canMove = true;
                }
            }
            else
            {
                moveDir = moveDirX;
                canMove = true;
            }
        }

        if(canMove)
            transform.position += moveDistance * moveDir;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, this.rotationSpeed * Time.deltaTime);
    }

    private Vector3 CanMoveOnDir(Vector3 moveDir, float moveDistance)
    {
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        return canMove ? moveDir : Vector3.zero;
    }
}
