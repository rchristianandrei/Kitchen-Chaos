using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    private void Update()
    {
        this.PlayerMovement();
    }

    private void PlayerMovement()
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
        transform.position += this.movementSpeed * Time.deltaTime * moveDir;
    }
}
