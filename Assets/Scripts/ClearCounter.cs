using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private SelectedCounterVisual selectedCounterVisual;

    public void Interact()
    {
        Debug.Log("interact");
    }

    public void Select(bool selected)
    {
        this.selectedCounterVisual.SetSelect(selected);
    }
}
