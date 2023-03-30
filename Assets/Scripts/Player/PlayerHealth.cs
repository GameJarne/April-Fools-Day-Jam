using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private const string INSTANT_DEATH_TAG = "Instant Death";

    [SerializeField] private float restartTime = 3f;

    public UnityEvent onDeath;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered a trigger");
        if (other.gameObject.CompareTag(INSTANT_DEATH_TAG))
        {
            Debug.Log("Player has died.");
            onDeath?.Invoke();
        }
    }

    private IEnumerator RestartAfterTime()
    {
        yield return new WaitForSeconds(restartTime);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Single);
    }
}
