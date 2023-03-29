using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LivingAsteroid : MonoBehaviour
{
    [SerializeReference] private Mesh disguisedMesh;
    [SerializeReference] private Mesh normalMesh;
    [SerializeReference] private MeshFilter meshFilter;

    private bool isDisguised;
    private Transform playerTransform;

    private void Start()
    {
        isDisguised = true;
        meshFilter.mesh = disguisedMesh;
    }

    private void Update()
    {
        if (!isDisguised)
        {
            transform.LookAt(playerTransform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTransform = other.transform;
            ToggleDisguise();
        }
    }

    private void ToggleDisguise()
    {
        meshFilter.mesh = isDisguised ? normalMesh : disguisedMesh;
        isDisguised = !isDisguised;
    }
}
