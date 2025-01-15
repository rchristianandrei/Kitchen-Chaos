using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;

    [SerializeField] private Transform recipeUIContainer;
    [SerializeField] private Transform recipeUITemplate;

    private void Awake()
    {
        recipeUITemplate.gameObject.SetActive(false);   
    }

    private void Start()
    {
        deliveryManager.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned; ;
        deliveryManager.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted; ;
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in recipeUIContainer)
        {
            if (child == recipeUITemplate) continue;

            Destroy(child.gameObject);
        }

        foreach (var recipeSO in DeliveryManager.Instance.GetRecipeSOList())
        {
            var newUIRecipe = Instantiate(recipeUITemplate, recipeUIContainer);

            var newUISettings = newUIRecipe.GetComponent<DeliveryManagerSingleUI>();
            newUISettings.SetRecipeName(recipeSO);

            newUIRecipe.gameObject.SetActive(true);
        }
    }
}
