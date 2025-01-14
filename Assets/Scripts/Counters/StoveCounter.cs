using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<float> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public StoveState state;
    }

    public enum StoveState
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private StoveState state;

    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;

    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Update()
    {
        if (!HasKitchenObject()) return;

        switch (state)
        {
            case StoveState.Idle:
                break;

            case StoveState.Frying:

                fryingTimer += Time.deltaTime;

                OnProgressChanged?.Invoke(this, fryingTimer / fryingRecipeSO.fryingTimerMax);
                if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                {
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                    burningTimer = 0;
                    burningRecipeSO = GetBurningRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = StoveState.Fried;
                    InvokeOnStateChangedEvent();
                }

                break;

            case StoveState.Fried:

                burningTimer += Time.deltaTime;

                OnProgressChanged?.Invoke(this, burningTimer / burningRecipeSO.burningTimerMax);
                if (burningTimer >= burningRecipeSO.burningTimerMax)
                {
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                    state = StoveState.Burned;
                    InvokeOnStateChangedEvent();
                }

                break;

            case StoveState.Burned:
                break;
        }
    }

    public override void Interact(Player player)
    {
        if (base.HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                // Give kitchen object to player
                base.GetKitchenObject().SetKitchenObjectParent(player);

                state = StoveState.Idle;
                InvokeOnStateChangedEvent();
                OnProgressChanged?.Invoke(this, 0);
            }
        }
        else
        {
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                // Get kitchen object from player
                player.GetKitchenObject().SetKitchenObjectParent(this);

                fryingTimer = 0;
                fryingRecipeSO = GetFryingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                state = StoveState.Frying;
                InvokeOnStateChangedEvent();
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        var recipe = GetFryingRecipeWithInput(input);

        return recipe != null;
    }

    private KitchenObjectSO GetFryingRecipeSOWithInput(KitchenObjectSO input)
    {
        var recipe = GetFryingRecipeWithInput(input);

        return recipe != null ? recipe.output : null;
    }

    private FryingRecipeSO GetFryingRecipeWithInput(KitchenObjectSO input)
    {
        foreach (var fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input != input) continue;

            return fryingRecipeSO;
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectSO input)
    {
        foreach (var burningRecipe in burningRecipeSOArray)
        {
            if (burningRecipe.input != input) continue;

            return burningRecipe;
        }

        return null;
    }

    private void InvokeOnStateChangedEvent()
    {
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
    }
}
