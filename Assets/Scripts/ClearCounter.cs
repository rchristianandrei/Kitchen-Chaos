using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private SelectedCounterVisual selectedCounterVisual;

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private KitchenObject kitchenObject;

    public void Interact(IKitchenObjectParent kitchenObjectParent)
    {
        if (kitchenObject != null)
        {
            this.kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
            return;
        }

        var spawnedKitchenObject = Instantiate(kitchenObjectSO.prefab, counterTopPoint.position, counterTopPoint.rotation, counterTopPoint);
        spawnedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
    }

    public void Select(bool selected)
    {
        selectedCounterVisual.SetSelect(selected);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
