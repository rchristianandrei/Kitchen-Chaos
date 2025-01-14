using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject hasProgressGameObject;
    [SerializeField] private Image progressBar;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        gameObject.SetActive(false);
    }

    private void HasProgress_OnProgressChanged(object sender, float e)
    {
        gameObject.SetActive(e > 0 && e < 1);
        progressBar.fillAmount = e;
    }
}
