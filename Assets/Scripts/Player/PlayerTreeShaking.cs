using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTreeShaking : MonoBehaviour
{
    [SerializeField] private float checkDistance = 1.0f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform gfxTransform;

    [SerializeField] private GameObject eButtonIndicator;
    private Image eButtonIndicatorImage;

    private RaycastHit hitInfo;

    private void Awake()
    {
        eButtonIndicatorImage = eButtonIndicator.GetComponent<Image>();
    }

    private void Start()
    {
        eButtonIndicator.SetActive(false);
    }

    private void Update()
    {
        bool canShakeTree = CanShakeTree();
        if (canShakeTree)
        {
            canShakeTree = hitInfo.transform.parent.GetComponent<ZigZagTree>().allowShaking;

            if (canShakeTree)
            {
                eButtonIndicatorImage.color = ChangeColorAlpha(eButtonIndicatorImage.color, 1f);
            } else
            {
                eButtonIndicatorImage.color = ChangeColorAlpha(eButtonIndicatorImage.color, 0.5f);
            }

            eButtonIndicator.SetActive(true);
        }
        else
        {
            eButtonIndicator.SetActive(false);
            eButtonIndicatorImage.color = ChangeColorAlpha(eButtonIndicatorImage.color, 1f);
        }

        

        if (Input.GetKeyDown(KeyCode.E) && canShakeTree)
        {
            hitInfo.transform.parent.GetComponent<Interactable>().OnInteract();
        }
    }

    private bool CanShakeTree()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * 0.3f), gfxTransform.forward);

        if (Physics.Raycast(ray, out hitInfo, checkDistance))
        {
            if (hitInfo.transform.gameObject.layer == interactableLayer ||
                    hitInfo.transform.gameObject.CompareTag("ZigZagTree"))
            {
                return true;
            }
        }
        return false;
    }

    private Color ChangeColorAlpha(Color originalColor, float alpha)
    {
        originalColor.a = alpha;
        return originalColor;
    }
}
