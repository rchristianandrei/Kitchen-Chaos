using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public event EventHandler<OnTogglePauseEventArgs> OnTogglePause;
    public class OnTogglePauseEventArgs : EventArgs
    {
        public bool isPaused;
    }

    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 5f;
    private float countdownToStartTimer = 5f;
    private float gamePlayingToStartTimer;
    private float gamePlayingToStartTimerMax = 60f;

    private bool isGamePaused = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Kitchen Game Manager should be singleton");
        }

        Instance = this;

        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:

                waitingToStartTimer -= Time.deltaTime;

                if(waitingToStartTimer <= 0)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state });
                }

                break;

            case State.CountdownToStart:

                countdownToStartTimer -= Time.deltaTime;

                if (countdownToStartTimer <= 0)
                {
                    state = State.GamePlaying;
                    gamePlayingToStartTimer = gamePlayingToStartTimerMax;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state });
                }

                break;

            case State.GamePlaying:

                gamePlayingToStartTimer -= Time.deltaTime;

                if (gamePlayingToStartTimer <= 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state });
                }

                break;

            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public float GetGameplayingTimerNormalized()
    {
        return 1 - (gamePlayingToStartTimer / gamePlayingToStartTimerMax);
    }

    public bool IsCountdownToStart()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetToCountdownStartTimer()
    {
        return countdownToStartTimer;
    }


    private void TogglePauseGame()
    {
        if (!IsGamePlaying()) return;

        isGamePaused = !isGamePaused;

        Time.timeScale = isGamePaused ? 0f : 1f;

        OnTogglePause?.Invoke(this, new OnTogglePauseEventArgs { isPaused = isGamePaused });
    }

    public void UnPauseGame()
    {
        isGamePaused = true;
        TogglePauseGame();
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
