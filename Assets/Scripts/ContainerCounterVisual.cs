using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter = transform.parent.GetComponent<ContainerCounter>();
        containerCounter.OnOpenContainer += ContainerCounter_OnOpenContainer;
    }

    private void ContainerCounter_OnOpenContainer(object sender, System.EventArgs e)
    {
        this.animator.SetTrigger(OPEN_CLOSE);
    }
}
