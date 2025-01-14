using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter, IHasProgress
{
    public event EventHandler<float> OnProgressChanged;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private const float spawnPlateTimerMax = 4f;
    private float spawnPlateTimer = 0f;

    private void Update()
    {
        if (HasKitchenObject()) return;

        spawnPlateTimer += Time.deltaTime;
        OnProgressChanged?.Invoke(this, spawnPlateTimer/ spawnPlateTimerMax);

        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            OnProgressChanged?.Invoke(this, 0);

            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
        }
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        
    }
}
