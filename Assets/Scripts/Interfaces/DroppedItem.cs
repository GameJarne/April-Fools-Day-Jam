using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    protected void Despawn()
    {
        Destroy(gameObject);
    }

    public virtual void PickupItem()
    {
        Destroy(gameObject);
    }
}
