using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameUI;

    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeName(RecipeSO recipeSO)
    {
        recipeNameUI.text = recipeSO.name;

        foreach (var ingredient in recipeSO.ingredients) 
        {
            var template = Instantiate(iconTemplate, iconContainer);
            template.GetComponent<Image>().sprite = ingredient.sprite;
            template.gameObject.SetActive(true);
        }

    }
}
