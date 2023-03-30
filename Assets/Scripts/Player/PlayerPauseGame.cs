using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private PlayerInput playerInput;
    private bool isPaused = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.OnPauseButton.AddListener(OnPauseKeyPressed);

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        isPaused = false;
    }

    private void OnPauseKeyPressed() // when the player pressed the pause key
    {
        TogglePauseGame();
    }

    private void TogglePauseGame()
    {
        if (isPaused) // is paused
        {
            Time.timeScale = 1f;
        }
        else // is not paused
        {
            Time.timeScale = 0f;
        }

        pauseMenu.SetActive(!isPaused);
        isPaused = !isPaused;
    }

    public void ResumeGame()
    {
        TogglePauseGame();
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
