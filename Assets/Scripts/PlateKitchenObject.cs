using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] List<KitchenObjectSO> validKitchenObjectSOList;

    private readonly List<KitchenObjectSO> kitchenObjectSOList = new();

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO) || !validKitchenObjectSOList.Contains(kitchenObjectSO)) return false;

        kitchenObjectSOList.Add(kitchenObjectSO);
        this.OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO });
        return true;
    }

    public static bool TryAddingIngredientToPlate(Player player, BaseCounter counter)
    {
        PlateKitchenObject plateKitchenObject;
        KitchenObject ingredient = null;

        if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject))
        {
            ingredient = counter.GetKitchenObject();
        }
        else if (counter.GetKitchenObject().TryGetPlate(out plateKitchenObject))
        {
            ingredient = player.GetKitchenObject();
        }

        if (plateKitchenObject != null)
        {
            if (plateKitchenObject.TryAddIngredient(ingredient.GetKitchenObjectSO()))
            {
                ingredient.DestroySelf();
                return true;
            }
        }

        return false;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
