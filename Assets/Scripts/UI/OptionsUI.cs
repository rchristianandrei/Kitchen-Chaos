using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    public event EventHandler OnOptionsClosed;

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private TextMeshProUGUI moveUpText;

    [SerializeField] private Button moveDownButton;
    [SerializeField] private TextMeshProUGUI moveDownText;

    [SerializeField] private Button moveLeftButton;
    [SerializeField] private TextMeshProUGUI moveLeftText;

    [SerializeField] private Button moveRightButton;
    [SerializeField] private TextMeshProUGUI moveRightText;

    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI interactText;

    [SerializeField] private Button altInteractButton;
    [SerializeField] private TextMeshProUGUI altInteractText;

    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Button closeButton;

    [SerializeField] private Transform pressToRebindKey;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Options UI should be singleton");

        Instance = this;
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnTogglePause += KitchenGameManager_OnTogglePause;

        UpdateVisual();

        sfxSlider.onValueChanged.AddListener((float arg0) =>
        {
            SoundManager.Instance.ChangeVolume(arg0 );
        });

        musicSlider.onValueChanged.AddListener((float arg0) =>
        {
            MusicManager.Instance.SetVolume(arg0);
        });

        moveUpButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Move_Left);});
        moveRightButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Move_Right);});
        interactButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Interact);});
        altInteractButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.InteractAlternative);});
        pauseButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Pause);});

        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            OnOptionsClosed?.Invoke(this, EventArgs.Empty);
        });

        pressToRebindKey.gameObject.SetActive(false);
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

    private void UpdateVisual()
    {
        sfxSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        altInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternative);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        pressToRebindKey.gameObject.SetActive(true);

        GameInput.Instance.RebindBinding(binding, () => { pressToRebindKey.gameObject.SetActive(false); UpdateVisual(); });
    }
}
