using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerTreeShaking : MonoBehaviour
{
    [SerializeField] private float checkDistance = 1.0f;
    [SerializeField] private float checkRadius = 2.0f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform gfxTransform;

    [SerializeField] private GameObject eButtonIndicator;

    private RaycastHit hitInfo;

    private void Start()
    {
        eButtonIndicator.SetActive(false);
    }

    private void Update()
    {
        bool canShakeTree = CanShakeTree();

        eButtonIndicator.SetActive(canShakeTree);

        if (Input.GetKeyDown(KeyCode.E) && canShakeTree)
        {
            hitInfo.transform.parent.GetComponent<Interactable>().OnInteract();
        }
    }

    private bool CanShakeTree()
    {
        Ray ray = new Ray(transform.position, gfxTransform.forward);

        return Physics.SphereCast(ray, checkRadius, out hitInfo, checkDistance, interactableLayer);
    }
}
