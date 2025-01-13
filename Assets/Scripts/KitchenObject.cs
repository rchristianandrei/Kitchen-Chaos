using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter currentClearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public ClearCounter GetClearCounter()
    {
        return currentClearCounter;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (currentClearCounter != null)
        {
            if (currentClearCounter.HasKitchenObject())
            {
                currentClearCounter.ClearKitchenObject();
            }
        }

        currentClearCounter = clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Clear Counter already has kitchen object");
        }

        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
}
