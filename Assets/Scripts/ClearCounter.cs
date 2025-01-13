using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private SelectedCounterVisual selectedCounterVisual;

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public void Interact()
    {
        var spawnedKitchenObject = Instantiate(kitchenObjectSO.prefab, counterTopPoint.position, counterTopPoint.rotation, counterTopPoint);

        Debug.Log(spawnedKitchenObject.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }

    public void Select(bool selected)
    {
        this.selectedCounterVisual.SetSelect(selected);
    }
}
