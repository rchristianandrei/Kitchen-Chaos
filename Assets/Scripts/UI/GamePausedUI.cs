using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
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

        gameObject.SetActive(false);
    }

    private void KitchenGameManager_OnTogglePause(object sender, KitchenGameManager.OnTogglePauseEventArgs e)
    {
        gameObject.SetActive(e.isPaused);
    }
}
