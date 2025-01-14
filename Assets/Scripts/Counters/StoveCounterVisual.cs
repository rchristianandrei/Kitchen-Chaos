using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static StoveCounter;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveSplashEffect;
    [SerializeField] private GameObject stoveOn;

    private StoveState[] OnStove = {
        StoveState.Frying, 
        StoveState.Fried
    };

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        var on = OnStove.Contains(e.state);

        stoveOn.SetActive(on);
        stoveSplashEffect.SetActive(on);

        //if(on)
        //    stoveSplashEffect.GetComponent<ParticleSystem>().Play();
        //else
        //    stoveSplashEffect.GetComponent<ParticleSystem>().Stop();
    }
}
