using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public void PlayLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }
}
