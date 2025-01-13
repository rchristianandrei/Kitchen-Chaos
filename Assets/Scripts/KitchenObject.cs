using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent currentKitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return currentKitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (currentKitchenObjectParent != null)
        {
            if (currentKitchenObjectParent.HasKitchenObject())
            {
                currentKitchenObjectParent.ClearKitchenObject();
            }
        }

        currentKitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has kitchen object");
        }

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.SetLocalPositionAndRotation(Vector3.zero, kitchenObjectParent.GetKitchenObjectFollowTransform().rotation);
    }

    public void DestroySelf()
    {
        currentKitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    /// <summary>
    /// Creates a new instacne of kitchen object and assigns it to the kitchen object parent.
    /// </summary>
    /// <param name="kitchenObjectSO">The kitchen object blueprint to spawn.</param>
    /// <param name="parent">The owner or holder of kitchen object.</param>
    /// <returns>The newly created kitchen object.</returns>
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
    {
        var spawned = Instantiate(kitchenObjectSO.prefab);

        var kitchenObject = spawned.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);

        return kitchenObject;
    }
}
