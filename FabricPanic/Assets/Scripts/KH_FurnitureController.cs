using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //need this to use list.GroupBy

public class KH_FurnitureController : MonoBehaviour
{
    [TextArea]
    public string instruction = "Add this to the furniture Prefab.\n" +
        "Children must follow structure PlacementSlots -> PlacingCollider";

    [SerializeField]
    private FPTags.FurnitureTag furniture_tag_;
    private List<FPTags.ObjectTag> object_list_ = new List<FPTags.ObjectTag>();
    private int furniture_score_;
    private int num_placeable_slots_ = 0;

    private void Awake()
    {
        num_placeable_slots_ = transform.GetChild(0).childCount;
    }

    public FPTags.FurnitureTag GetTag()
    {
        return furniture_tag_;
    }

    public void AddToObjList(FPTags.ObjectTag value)
    {
        object_list_.Add(value);
        UpdateGameScore();
    }

    public void RemoveFromObjList(FPTags.ObjectTag value)
    {
        object_list_.Remove(value);
        UpdateGameScore();
    }

    public void UpdateGameScore()
    {
        int temp_score = 0;
        
        var tag_count_dict = object_list_.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count()); //https://stackoverflow.com/questions/20069589/counting-the-number-of-occurrences-of-every-distinct-value-of-a-list

        foreach (var tag_count in tag_count_dict.Values) //https://stackoverflow.com/questions/141088/what-is-the-best-way-to-iterate-over-a-dictionary
        {
            if(tag_count > 1) // if more than one object on furniture
            {
                temp_score += 20 * tag_count; // 20 points for any adjacent objects with same object tag on this furniture
            }
        }

        furniture_score_ = temp_score;
    }

    public int GetScore()
    {
        return furniture_score_;
    }
    
    public int GetNumPlaceableSlots()
    {
        return num_placeable_slots_;
    }


}
