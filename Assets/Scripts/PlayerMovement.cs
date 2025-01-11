using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 10f;

    public bool IsWalking { get; private set; }

    private void Update()
    {
        this.Movement();
    }

    private void Movement()
    {
        Vector2 input = new();

        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }

        input = input.normalized;
        Vector3 moveDir = new(input.x, 0, input.y);

        this.IsWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, this.rotationSpeed * Time.deltaTime);
        transform.position += this.movementSpeed * Time.deltaTime * moveDir;
    }
}
