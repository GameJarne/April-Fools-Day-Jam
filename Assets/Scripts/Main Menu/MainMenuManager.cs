using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelsPanel;

    private void Start()
    {
        Time.timeScale = 1f;

        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }

    public void OnPlayButton()
    {
        levelsPanel.SetActive(true);
    }
    public void OnExitLevelManagerButton()
    {
        levelsPanel.SetActive(false);
    }

    public void OnSettingsButton()
    {
        settingsPanel.SetActive(true);
    }
    public void OnExitSettingsButton()
    {
        settingsPanel.SetActive(false);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
