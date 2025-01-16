using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    public event EventHandler OnOptionsClosed;

    [SerializeField] private Button closeButton;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Options UI should be singleton");

        Instance = this;
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnTogglePause += KitchenGameManager_OnTogglePause;

        sfxSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();

        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            OnOptionsClosed?.Invoke(this, EventArgs.Empty);
        });

        sfxSlider.onValueChanged.AddListener((float arg0) =>
        {
            SoundManager.Instance.ChangeVolume(arg0 );
        });

        musicSlider.onValueChanged.AddListener((float arg0) =>
        {
            MusicManager.Instance.SetVolume(arg0);
        });

        gameObject.SetActive(false);
    }

    private void KitchenGameManager_OnTogglePause(object sender, KitchenGameManager.OnTogglePauseEventArgs e)
    {
        if (e.isPaused) return;

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if (Instance == null) return;

        Instance = null;
    }
}
