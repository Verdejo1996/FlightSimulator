using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    public void GameQuit()
    {
        Application.Quit();
    }

    public void StartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void BackMenu(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
