using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHover : MonoBehaviour
{
    [SerializeField] private float hoverAmount = 0.1f;
    [SerializeField] private bool canTurnIntoLiving = false;
    [SerializeField] private Transform livingAsteroidPrefab;

    private float offset;
    private Vector3 startPosition;

    private void Start()
    {
        offset = Random.Range(-1f, 1f);
        hoverAmount = hoverAmount + (0.1f * (Random.Range(-2, 2)));
        transform.localRotation = Random.rotation;

        startPosition = transform.position;

        if (canTurnIntoLiving)
        {
            if (Random.Range(0, 10) == 0)
            {
                Transform livingAsteroid = Instantiate(livingAsteroidPrefab);

                livingAsteroid.position = startPosition;
                livingAsteroid.GetComponent<LivingAsteroid>().startingPosition = startPosition;

                Destroy(this.gameObject);
            }
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startPosition.y + Mathf.Cos(Time.time) * hoverAmount + offset, transform.position.z);
    }
}
