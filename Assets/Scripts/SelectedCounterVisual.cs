using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject counterSelectVisual;

    public void SetSelect(bool active)
    {
        this.counterSelectVisual.SetActive(active);
    }
}
