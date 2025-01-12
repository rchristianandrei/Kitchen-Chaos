using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private GameInput gameInput;

    public bool IsWalking { get; private set; }

    private void Update()
    {
        this.Movement();
    }

    private void Movement()
    {
        Vector2 input = this.gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(input.x, 0, input.y);

        this.IsWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, this.rotationSpeed * Time.deltaTime);
        transform.position += this.movementSpeed * Time.deltaTime * moveDir;
    }
}
