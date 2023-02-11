using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string MainGameSceneName = "Shooting";

    public void StartGame()
    {
        SceneManager.LoadScene(MainGameSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
