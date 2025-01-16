using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SFX_VOLUME = "sfxVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1.0f;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Sound Manager should be singleton");
        }
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;

        Player.Instance.OnPickedSomething += Player_OnPickedSomething;

        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;

        TrashCounter.OnDisposeObject += TrashCounter_OnDisposeObject;
    }

    private void TrashCounter_OnDisposeObject(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.trash, (sender as TrashCounter).transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectDrop, (sender as BaseCounter).transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickUp, (sender as Player).transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.chop, (sender as CuttingCounter).transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClip[Random.Range(0, audioClip.Length)], position, volumeMultiplier * this.volume);
    }

    public void PlayFootstepsSound(Player player)
    {
        PlaySound(audioClipRefsSO.footsteps, player.transform.position);
    }

    public void ChangeVolume(float volume) {
        this.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}
