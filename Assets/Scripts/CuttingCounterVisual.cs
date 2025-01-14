using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter = transform.parent.GetComponent<CuttingCounter>();
        cuttingCounter.CuttingCounterChanged += CuttingCounter_CuttingCounterChanged;
    }

    private void CuttingCounter_CuttingCounterChanged(object sender, float e)
    {
        if (e == 0) return;
        this.animator.SetTrigger(CUT);
    }
}
