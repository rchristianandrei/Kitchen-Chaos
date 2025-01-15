using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public event EventHandler<float> OnProgressChanged;

    private int cuttingProgress;

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
                // Give kitchen object to player
                base.GetKitchenObject().SetKitchenObjectParent(player);
                this.InvokeCuttingChangedEvent(0f);

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                // Get kitchen object from player
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
                this.InvokeCuttingChangedEvent(0f);
                RaiseOnAnyObjectPlacedEvent();
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (!base.HasKitchenObject()) return;

        var kitchenObject = base.GetKitchenObject();
        var input = kitchenObject.GetKitchenObjectSO();
        var cuttingRecipe = this.GetCuttingRecipeForInput(input);

        if (cuttingRecipe == null) return;

        cuttingProgress++;
        this.InvokeCuttingChangedEvent((float)cuttingProgress / cuttingRecipe.cuttingProgressMax);
        if (cuttingProgress < cuttingRecipe.cuttingProgressMax) return;

        kitchenObject.DestroySelf();
        KitchenObject.SpawnKitchenObject(cuttingRecipe.output, this);
    }

    private CuttingRecipeSO GetCuttingRecipeForInput(KitchenObjectSO input)
    {
        foreach (var cuttingRecipe in cuttingRecipeSOArray) {
            if (cuttingRecipe.input != input) continue;
            return cuttingRecipe;
        }

        return null;
    }

    private void InvokeCuttingChangedEvent(float progress)
    {
        OnProgressChanged?.Invoke(this, progress);
        OnAnyCut?.Invoke(this, EventArgs.Empty);
    }
}
