
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int mode; // 0 - single, 1 - two

    public void assignMode(int mode)
    {
        MainMenu.mode = mode;
    }

    public void LoadLevel(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
