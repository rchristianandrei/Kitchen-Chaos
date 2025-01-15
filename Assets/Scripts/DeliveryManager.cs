using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private readonly List<RecipeSO> waitingRecipeSOList = new();
    private const int waitingRecipeSOListMax = 4;

    private float spawnRecipeTimer = 0f;
    private const float spawnRecipeTimerMax = 5f;

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
            var recipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
            waitingRecipeSOList.Add(recipeSO);

            Debug.Log(recipeSO.name);
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

            if (found) foundRecipeSOIndex = i;
        }

        if (found) {
            waitingRecipeSOList.RemoveAt(foundRecipeSOIndex);
            return true;
        }
        else
        {
            return false;
        }
    }
}
