using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance;

    public event EventHandler OnPickedSomething;

    public event EventHandler<CounterEventArgs> OnCounterChanged;
    public class CounterEventArgs 
    { 
        public BaseCounter counter { get; set; }
    }

    public bool IsWalking { get; private set; }

    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private LayerMask countersLayerMask;

    private Vector3 lastInteractDir;
    private BaseCounter currentCounter;

    #region "IKitchenObjectParent"
    [SerializeField] Transform heldKitchenObjectPoint;
    private KitchenObject kitchenObject;

    public Transform GetKitchenObjectFollowTransform()
    {
        return heldKitchenObjectPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    #endregion

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There should only be one player!");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        this.gameInput.OnInteractAction += GameInput_OnInteractAction;
        this.gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if(this.currentCounter != null ) this.currentCounter.Interact(this);
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (this.currentCounter != null) this.currentCounter.InteractAlternate(this);
    }

    private void Update()
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        this.FindICounter();
        this.Movement();
        this.Rotation();
    }

    private void FindICounter()
    {
        Vector2 input = this.gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(input.x, 0, input.y);
         
        if (moveDir != Vector3.zero) this.lastInteractDir = moveDir;

        float interactDistance = 2f;
        if (!Physics.Raycast(transform.position, this.lastInteractDir, out RaycastHit hitInfo, interactDistance, this.countersLayerMask))
        {
            if (this.currentCounter != null) this.RaiseOnCounterChanged(null);
            this.currentCounter = null;
            return;
        }

        // Check if counter
        if (!hitInfo.transform.TryGetComponent(out BaseCounter clearCounter)) return;

        // Check if need to change selected counter
        if (this.currentCounter == clearCounter) return;

        // Uncheck if previously selected is not null
        if (this.currentCounter != null) this.RaiseOnCounterChanged(null);

        this.currentCounter = clearCounter;
        this.RaiseOnCounterChanged(clearCounter);
    }

    private void RaiseOnCounterChanged(BaseCounter e)
    {
        if(OnCounterChanged == null) return;

        OnCounterChanged.Invoke(this, new CounterEventArgs { counter = e});
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
            canMove = moveDir.x < -5 && moveDir.x > 5 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Check Y
                var moveDirY = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.y < -5 && moveDir.y > 5 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirY, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirY;
                }
            }
        }

        if (!canMove) return;

        transform.position += moveDistance * moveDir;
    }

    private void Rotation()
    {
        Vector2 input = this.gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(input.x, 0, input.y);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, this.rotationSpeed * Time.deltaTime);
    }
}
