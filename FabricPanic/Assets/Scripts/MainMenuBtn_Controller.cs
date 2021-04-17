using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtn_Controller : MonoBehaviour
{
    public void LaunchMainMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
