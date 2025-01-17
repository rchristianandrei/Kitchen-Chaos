using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnTogglePause += KitchenGameManager_OnTogglePause;

        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.UnPauseGame();
        });

        OptionsUI.Instance.OnOptionsClosed += OptionsUI_OnOptionsClosed;
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }

    private void OptionsUI_OnOptionsClosed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void KitchenGameManager_OnTogglePause(object sender, KitchenGameManager.OnTogglePauseEventArgs e)
    {
        gameObject.SetActive(e.isPaused);

        if(e.isPaused) resumeButton.Select();
    }
}
