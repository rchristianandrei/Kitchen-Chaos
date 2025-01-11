using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField] 
    private Animator animator;

    private void Update()
    {
        animator.SetBool(IS_WALKING, this.playerMovement.IsWalking);
    }
}
