using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KH_ObjectSpawnerController : MonoBehaviour
{
    public string instruction = "1) Add this script to every GameObject you want to use drag & drop\n" +
        "2) Pre-defined placement slots have to be set to layer \"Ignore Raycast\"\n" +
        "3) Set Object Tag\n" +
        "4) Add Collider";

    [SerializeField]
    private Transform placeable_obj_group_;
    private List<GameObject> placeable_obj_list_ = new List<GameObject>();
    private GameObject curr_clone = null;
    [SerializeField]
    private float clone_move_speed_ = 3f;
    private Transform spawn_point;
    private Transform move_to_point;
    private float spawn_rate_ = 3.0f;
    private float curr_time_;

    // Start is called before the first frame update
    void Awake()
    {
        // SET SPAWN POINT
        foreach (Transform child in transform)
        {
            if (child.name == "SpawnPoint")
            {
                spawn_point = child;
            }
            else
            {
                move_to_point = child;
            }
        }

        // SET OBJ LIST
        foreach (Transform child in placeable_obj_group_)
        {
            placeable_obj_list_.Add(child.gameObject);
        }

        FPGlobalSwitches.can_spawn_objects = true;
    }

    // Update is called once per frame
    void Update()
    {
        curr_time_ -= Time.deltaTime;
        if (FPGlobalSwitches.can_spawn_objects && curr_time_<0) // if new object is ready to be spawned and timer is zeroed
        {
            // SPAWN RANDOM OBJ 
            int index = Random.Range(0, placeable_obj_group_.childCount - 1);
            Debug.Log(placeable_obj_list_[index].name);
            curr_clone = Instantiate(placeable_obj_list_[index]);
            curr_clone.transform.position = spawn_point.position;
            curr_clone.GetComponent<KH_DragAndDropController>().starting_pos = move_to_point.position;

            FPGlobalSwitches.can_spawn_objects = false;
            curr_time_ = spawn_rate_;
        }

        if(curr_clone!= null)
        {
            // MOVE CLONE OUT OF BOX
            if(!curr_clone.GetComponent< KH_DragAndDropController>().can_be_dragged)
            {
                curr_clone.transform.position = Vector3.MoveTowards(curr_clone.transform.position, move_to_point.position, clone_move_speed_ * Time.deltaTime);
            }

            // IF CLONE HAS BEEN MOVED OUT OF BOX, LET PLAYER INTERACT
            if(curr_clone.transform.position == move_to_point.position)
            {
                curr_clone.GetComponent<KH_DragAndDropController>().can_be_dragged = true;
            }
        }
        
    }
}
