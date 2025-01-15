using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) {
            Debug.Log("Delivery Manager should be singleton");
        }

        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) return;

        if (!player.GetKitchenObject().TryGetPlate(out var plate)) return;

        // Validate if this is plate is part of the order
        var valid = DeliveryManager.Instance.TryDeliverRecipe(plate);

        if (!valid) return;

        plate.DestroySelf();

        // Success!
    }

    public override void InteractAlternate(Player player)
    {
        
    }
}
