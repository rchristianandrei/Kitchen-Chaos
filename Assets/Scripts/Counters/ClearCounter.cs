using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        // Get kitchen object
        if (player.HasKitchenObject() && !base.HasKitchenObject())
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            return;
        }

        // Give kitchen object
        if (!player.HasKitchenObject() && base.HasKitchenObject()) { 
            base.GetKitchenObject().SetKitchenObjectParent(player);
            return;
        }
    }

    public override void InteractAlternate(Player player)
    {

    }
}
