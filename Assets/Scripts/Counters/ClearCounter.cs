using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                PlateKitchenObject.TryAddingIngredientToPlate(player, this);
            }
            else
            {
                // Give kitchen object
                base.GetKitchenObject().SetKitchenObjectParent(player);
                return;
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                // Get kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
                return;
            }
            else
            {

            }
        }
    }

    public override void InteractAlternate(Player player)
    {

    }
}
