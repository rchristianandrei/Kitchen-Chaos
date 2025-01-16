using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "musicVolume";

    public static MusicManager Instance { get; private set; }

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Music Manager should be singleton");

        Instance = this;

        musicSource = GetComponent<AudioSource>();

        musicSource.volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicSource.volume);
    }

    public float GetVolume()
    {
        return musicSource.volume;
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
    }

    private void OnDestroy()
    {
        if (Instance == null) return;

        Instance = null;
    }
}
