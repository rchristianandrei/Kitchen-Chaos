using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO recipeListSO;

    private readonly List<RecipeSO> waitingRecipeSOList = new();
    private const int waitingRecipeSOListMax = 4;

    private float spawnRecipeTimer = 0f;
    private const float spawnRecipeTimerMax = 5f;

    private int recipesDelivered = 0;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Delivery Manager should be singleton");
        }
        Instance = this;
    }

    private void Update()
    {
        if (waitingRecipeSOList.Count >= waitingRecipeSOListMax) return;

        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            // Spawn an oder.
            var recipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
            waitingRecipeSOList.Add(recipeSO);

            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool TryDeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        int foundRecipeSOIndex = 0;
        bool found = false;

        for (var i = 0; i < waitingRecipeSOList.Count; i++)
        {
            var recipeSO = waitingRecipeSOList[i];

            if (plateKitchenObject.GetKitchenObjectSOList().Count != recipeSO.ingredients.Count) continue;

            found = true;
            foreach (var ingredient in recipeSO.ingredients)
            {
                if (plateKitchenObject.GetKitchenObjectSOList().Contains(ingredient)) continue;

                found = false;
                break;
            }

            if (!found) continue;

            foundRecipeSOIndex = i;
            break;
        }

        if (found) {
            waitingRecipeSOList.RemoveAt(foundRecipeSOIndex);
            OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
            OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
            recipesDelivered++;
            return true;
        }
        else
        {
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
            return false;
        }
    }

    public List<RecipeSO> GetRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetRecipesDelivered()
    {
        return recipesDelivered;
    }
}
