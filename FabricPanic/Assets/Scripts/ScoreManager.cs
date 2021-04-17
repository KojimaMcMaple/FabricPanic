using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager:MonoBehaviour
{
    private int game_score = 0;
    private GameObject[] furniture_obj_list_;

    private void Awake()
    {
        furniture_obj_list_ = GameObject.FindGameObjectsWithTag("Furniture");
    }

    private void Update()
    {
        int temp_score = 0;
        if (FPGlobalSwitches.needs_score_update)
        {
            foreach (GameObject furni in furniture_obj_list_)
            {
                temp_score += furni.GetComponent<KH_FurnitureController>().GetScore();
            }
        }

        game_score = temp_score;
    }

    public int GetGameScore()
    {
        return game_score;
    }

    public void IncrementGameScore(int value)
    {
        game_score += value;
    }
}
