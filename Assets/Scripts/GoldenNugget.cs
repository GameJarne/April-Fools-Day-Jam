using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenNugget : DroppedItem
{
    [SerializeField] private float timeUntilDespawn = 5f;
    private float timeSinceSpawned = 0f;

    private void Start()
    {
        timeSinceSpawned = 0f;
    }

    private void Update()
    {
        timeSinceSpawned += Time.deltaTime;

        if (timeSinceSpawned > timeUntilDespawn)
        {
            Despawn();
        }
    }
}
