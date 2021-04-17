using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introController : MonoBehaviour
{
    [SerializeField]
    private GameObject title_ui_;
    [SerializeField]
    private GameObject main_menu_panel_;

    private void Awake()
    {
        //DISABLE COMPONENTS BEFORE SCENE IS SHOWN 
        main_menu_panel_.SetActive(false);
        title_ui_.SetActive(true);
    }

    public void LoadMenu()
    {
        main_menu_panel_.SetActive(true);
        title_ui_.SetActive(false);
    }

    public void MoveToGame()
    {
        SceneManager.LoadScene(1);
    }
}
