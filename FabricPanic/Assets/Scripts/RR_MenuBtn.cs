using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RR_MenuBtn : MonoBehaviour
{
    public GameObject pauseScreen = null;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
    public void LaunchMainMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
