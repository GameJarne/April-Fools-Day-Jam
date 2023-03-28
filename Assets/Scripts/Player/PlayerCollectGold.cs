using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollectGold : MonoBehaviour
{
    private const string GOLD_NUGGET_TAG = "Gold Nugget";

    [SerializeField] private TMP_Text goldCollectedText;

    private int goldCollected = 0;

    private void Start()
    {
        goldCollected = 0;
        goldCollectedText.text = $"Gold: {goldCollected}";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GOLD_NUGGET_TAG))
        {
            other.gameObject.GetComponent<DroppedItem>().PickupItem();
            goldCollected++;
            goldCollectedText.text = $"Gold: {goldCollected}";
        }
    }
}
