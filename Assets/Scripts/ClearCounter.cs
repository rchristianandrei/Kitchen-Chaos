using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (base.HasKitchenObject())
        {
            base.GetKitchenObject().SetKitchenObjectParent(player);
            return;
        }

        var spawnedKitchenObject = Instantiate(kitchenObjectSO.prefab);
        spawnedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
    }
}
