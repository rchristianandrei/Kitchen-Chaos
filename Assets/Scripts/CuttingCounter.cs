using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKicthenObjectSO;

    public override void Interact(Player player)
    {
        // Get kitchen object
        if (player.HasKitchenObject() && !base.HasKitchenObject())
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            return;
        }

        // Give kitchen object
        if (!player.HasKitchenObject() && base.HasKitchenObject())
        {
            base.GetKitchenObject().SetKitchenObjectParent(player);
            return;
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (!base.HasKitchenObject()) return;

        base.GetKitchenObject().DestroySelf();

        KitchenObject.SpawnKitchenObject(cutKicthenObjectSO, this);
    }
}
