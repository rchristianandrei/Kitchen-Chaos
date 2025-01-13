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
            if (!player.HasKitchenObject())
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
                var spawnedKitchenObject = Instantiate(kitchenObjectSO.prefab);
                spawnedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                OnOpenContainer?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
