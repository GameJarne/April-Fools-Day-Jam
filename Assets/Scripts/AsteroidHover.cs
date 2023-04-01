using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHover : MonoBehaviour
{
    [SerializeField] private float hoverAmount = 0.1f;

    private float offset;
    private Vector3 startPosition;

    private void Start()
    {
        offset = Random.Range(-1f, 1f);
        hoverAmount = hoverAmount + (0.1f * (Random.Range(-2, 2)));

        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startPosition.y + Mathf.Cos(Time.time) * hoverAmount + offset, transform.position.z);
    }
}
