using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjectSOGameObjectList;
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject obj in KitchenObjectSOGameObjectList)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject obj in KitchenObjectSOGameObjectList)
        {
            if (obj.kitchenObjectSO == e.kitchenObjectSO)
            {
                obj.gameObject.SetActive(true);
                return;
            }
        }
    }
}
