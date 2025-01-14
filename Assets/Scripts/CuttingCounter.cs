using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

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

        var kitchenObject = base.GetKitchenObject();
        var input = kitchenObject.GetKitchenObjectSO();
        var output = this.GetOutputForInput(input);

        if (output == null) return;

        kitchenObject.DestroySelf();
        KitchenObject.SpawnKitchenObject(output, this);
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        foreach (var cuttingRecipe in cuttingRecipeSOArray) {
            if (cuttingRecipe.input != input) continue;
            return cuttingRecipe.output;
        }

        return null;
    }
}
