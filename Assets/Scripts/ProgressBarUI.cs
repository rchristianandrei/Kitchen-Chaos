using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image progressBar;

    private void Start()
    {
        cuttingCounter.CuttingCounterChanged += CuttingCounter_CuttingCounterChanged;

        gameObject.SetActive(false);
    }

    private void CuttingCounter_CuttingCounterChanged(object sender, float e)
    {
        gameObject.SetActive( e > 0 && e < 1);
        progressBar.fillAmount = e;
    }
}
