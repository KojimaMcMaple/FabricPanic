using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBtn_Controller : MonoBehaviour
{
    public void LaunchNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
