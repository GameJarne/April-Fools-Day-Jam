using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private const string INSTANT_DEATH_TAG = "Instant Death";
    private const string LIVING_ASTEROID_TAG = "Living Asteroid";

    [SerializeField] private float restartTime = 3f;
    [SerializeField] private GameObject playerGraphics;
    [SerializeField] private GameObject playerRagdollPrefab;

    public UnityEvent onDeath;

    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(INSTANT_DEATH_TAG) && !isDead)
        {
            isDead = true;
            SetRagdoll();
            onDeath?.Invoke();
            StartCoroutine(RestartAfterTime());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(LIVING_ASTEROID_TAG) && !isDead)
        {
            isDead = true;
            SetRagdoll();
            onDeath?.Invoke();
            StartCoroutine(RestartAfterTime());
        }
    }

    private void SetRagdoll()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        playerGraphics.SetActive(false);
        GameObject ragdoll = Instantiate(playerRagdollPrefab, this.transform);
        ragdoll.transform.position = transform.position;
        ragdoll.transform.localRotation = playerGraphics.transform.localRotation;
    }

    private IEnumerator RestartAfterTime()
    {
        yield return new WaitForSeconds(restartTime);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Single);
    }
}
