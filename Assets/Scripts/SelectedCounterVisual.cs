using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject counterSelectVisual;
    [SerializeField] private BaseCounter counter;

    private void Start()
    {
        Player.Instance.OnCounterChanged += Instance_OnCounterChanged;
    }

    private void Instance_OnCounterChanged(object sender, Player.CounterEventArgs e)
    {
        counterSelectVisual.SetActive(e.counter == counter);
    }

    public void SetSelect(bool active)
    {
        this.counterSelectVisual.SetActive(active);
    }
}
