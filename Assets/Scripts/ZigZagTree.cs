using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagTree : MonoBehaviour, Interactable
{
    private const string SHAKING_ANIM_NAME = "Shaking";

    [SerializeField] private Transform goldSpawnLocation;
    [SerializeField] private Transform goldNuggetPrefab;
    [SerializeField] private int maxGoldNuggets = 3;
    private int goldNuggetsDropped = 0;
    private Animator animator;

    [HideInInspector] public bool allowShaking = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        goldNuggetsDropped = 0;
    }

    public void OnInteract()
    {
        if (allowShaking)
        {
            animator.Play(SHAKING_ANIM_NAME);

            if (goldNuggetsDropped < maxGoldNuggets)
            {
                goldNuggetsDropped++;
                SpawnGoldNugget();
            }

            allowShaking = false;
            StartCoroutine(ResetAllowShaking());
        }
    }

    IEnumerator ResetAllowShaking()
    {
        yield return new WaitForSeconds(5f);
        allowShaking = true;
    }

    void SpawnGoldNugget()
    {
        Transform goldNugget = Instantiate(goldNuggetPrefab);
        goldNugget.transform.SetLocalPositionAndRotation(goldSpawnLocation.position, goldSpawnLocation.rotation);
    }
}
