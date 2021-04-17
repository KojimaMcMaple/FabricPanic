using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KH_DragAndDropController : MonoBehaviour
{
    /* NOTES:
     * https://youtu.be/0yHBDZHLRbQ does NOT work since y is modified, and object not matching cursor if y is locked
    */

    [TextArea]
    public string instruction = "1) Add this script to every GameObject you want to use drag & drop\n" +
        "2) Pre-defined placement slots have to be set to layer \"Ignore Raycast\"\n" +
        "3) Set Object Tag\n" +
        "4) Add Collider";

    [SerializeField]
    private FPTags.ObjectTag object_tag_;

    private Collider coll;
    public Vector3 starting_pos = Vector3.zero;
    private Plane raycast_plane;
    private int mask_to_collide_with;
    private Collider containing_coll_;
    public bool can_be_dragged = false;

    private void Start()
    {
        if(starting_pos == Vector3.zero) //KH_ObjectSpawnerController will set starting pos, if not, default to default pos
        {
            starting_pos = transform.position; 
        }
        
        coll = GetComponent<Collider>();
        mask_to_collide_with = LayerMask.GetMask("Ignore Raycast"); //[IMPORTANT] placement colls have to be put in Ignore Raycast -
                                                                    //because OnMouseDown() only works with colls, so if gob is put inside a bigger coll,
                                                                    //Unity will only recognize the bigger coll and not the gob inside it.
    }

    private void OnMouseDown()
    {
        raycast_plane = new Plane(Vector3.up, -starting_pos.y); /*A negative distance value results in the Plane facing away from the origin. https://docs.unity3d.com/ScriptReference/Plane-ctor.html */
    }

    private void OnMouseDrag()
    {
        // How to get the mouse position in world space (without using Colliders) using a Plane https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#screen_to_world_2d
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (raycast_plane.Raycast(ray, out distance))
        {
            Vector3 new_pos = ray.GetPoint(distance);
            transform.position = new Vector3(new_pos.x, new_pos.y, new_pos.z);
        }
    }

    private void OnMouseUp()
    {
        // CHECK IF COLLIDED WITH mask_to_collide_with
        Collider[] hit_colliders = Physics.OverlapBox(coll.transform.position, coll.transform.localScale / 2, Quaternion.identity, mask_to_collide_with); //https://docs.unity3d.com/ScriptReference/Physics.OverlapBox.html
        Collider closest_coll = null;
        float shortest_coll_distance = Mathf.Infinity;
        
        // FIND CLOSEST COLL
        if(hit_colliders.Length > 0) 
        {
            float coll_distance;
            for (int i = 0; i < hit_colliders.Length; i++)
            {
                if (hit_colliders[i].transform.GetComponent<KH_ObjectPlacementSlotController>().IsEmpty()) //if slot is not already occupied
                {
                    Transform container_parent = hit_colliders[i].transform.GetComponent<KH_ObjectPlacementSlotController>().GetParent();
                    if (FPTags.IsObjectCompatible(object_tag_, container_parent.GetComponent<KH_FurnitureController>().GetTag()))
                    {
                        coll_distance = Vector3.Distance(coll.transform.position, hit_colliders[i].transform.position);
                        if (coll_distance < shortest_coll_distance)
                        {
                            shortest_coll_distance = coll_distance;
                            closest_coll = hit_colliders[i];
                        }
                    }
                    else
                    {
                        Debug.Log("Furniture is not compatible with object!");
                    }
                }
                else
                {
                    Debug.Log("Placement slot is occupied!");
                }
            }
        }

        // SET GOB POS
        if (closest_coll == null) //reset if not collided
        {
            transform.position = starting_pos;
        }
        else //if collided, set gob to center of collider & update starting_pos 
        {
            transform.position = new Vector3(closest_coll.transform.position.x,
                                                closest_coll.bounds.min.y, // no offset for Y as objects should stay above origin in export scene (unlike Unity cube default) 
                                                closest_coll.transform.position.z); // set gob flat at bottom of closest_coll

            starting_pos = transform.position;

            if (containing_coll_ != null) //if gob was in a placement slot previously, set prev containing_coll.is_empty and update curr containing_coll
            {
                containing_coll_.transform.GetComponent<KH_ObjectPlacementSlotController>().SetEmpty(true);
                containing_coll_.transform.GetComponent<KH_ObjectPlacementSlotController>().GetParent().GetComponent<KH_FurnitureController>().RemoveFromObjList(object_tag_);
            }
            containing_coll_ = closest_coll;
            closest_coll.transform.GetComponent<KH_ObjectPlacementSlotController>().SetEmpty(false);
            closest_coll.transform.GetComponent<KH_ObjectPlacementSlotController>().GetParent().GetComponent<KH_FurnitureController>().AddToObjList(object_tag_);

            FPGlobalSwitches.needs_score_update = true;

            // OBJECT SPAWNER CAN SPAWN IF OBJECT IS PLACED SUCCESSFULLY
            FPGlobalSwitches.can_spawn_objects = true;
        }
    }

    
}