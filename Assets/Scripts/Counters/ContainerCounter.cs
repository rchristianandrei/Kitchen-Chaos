using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnOpenContainer;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (base.HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                PlateKitchenObject.TryAddingIngredientToPlate(player, this);
            }
            else
            {
                // Give kitchen object
                base.GetKitchenObject().SetKitchenObjectParent(player);

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                // Put down kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Create new kitchen object
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                OnOpenContainer?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public override void InteractAlternate(Player player)
    {
        
    }
}
