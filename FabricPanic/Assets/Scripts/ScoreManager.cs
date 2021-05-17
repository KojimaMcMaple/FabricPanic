using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager:MonoBehaviour
{
    private int game_score_ = 0;
    private GameObject[] furniture_obj_list_;
    private int total_num_placeable_slots_ = 0;
    [SerializeField]
    private float game_end_placement_percentage_ = 80.0f;
    private int game_end_placement_value_ = 100;

    private void Awake()
    {
        furniture_obj_list_ = GameObject.FindGameObjectsWithTag("Furniture");
    }

    private void Start()
    {
        foreach (GameObject furni in furniture_obj_list_)
        {
            total_num_placeable_slots_ += furni.GetComponent<KH_FurnitureController>().GetNumPlaceableSlots();
        }
        game_end_placement_value_ = (int)(total_num_placeable_slots_ * game_end_placement_percentage_ / 100.0f);
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

        game_score_ = temp_score;
    }

    public int GetGameScore()
    {
        return game_score_;
    }

    public void IncrementGameScore(int value)
    {
        game_score_ += value;
    }
}
