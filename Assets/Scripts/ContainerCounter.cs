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
        if (player.HasKitchenObject()) return;
        var spawnedKitchenObject = Instantiate(kitchenObjectSO.prefab);
        spawnedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnOpenContainer?.Invoke(this, EventArgs.Empty);
    }
}
