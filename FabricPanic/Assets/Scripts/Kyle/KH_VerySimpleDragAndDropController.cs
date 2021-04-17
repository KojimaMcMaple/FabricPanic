using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KH_VerySimpleDragAndDropController : MonoBehaviour
{
    /* NOTES:
     * https://youtu.be/0yHBDZHLRbQ does NOT work since y is modified, and object not matching cursor if y is locked
    */

    [TextArea]
    public string Instruction = "Add this script to every GameObject you want to use drag & drop\n" +
        "Pre-defined placement slots have to be set to layer \"Ignore Raycast\"";



    private Collider coll;
    private Vector3 starting_pos;
    private Plane raycast_plane;
    private int mask_to_collide_with;

    private void Start()
    {
        starting_pos = transform.position;
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
        if (hit_colliders.Length > 0)
        {
            closest_coll = hit_colliders[0]; //if there's 1 hit_coll, it's the closest one
            shortest_coll_distance = Vector3.Distance(coll.transform.position, closest_coll.transform.position);
            if (hit_colliders.Length > 1) //if more than 1 hit_coll, find the closest one
            {
                float coll_distance;
                for (int i = 1; i < hit_colliders.Length; i++)
                {
                    coll_distance = Vector3.Distance(coll.transform.position, hit_colliders[i].transform.position);
                    if (coll_distance < shortest_coll_distance)
                    {
                        shortest_coll_distance = coll_distance;
                        closest_coll = hit_colliders[i];
                    }
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
                                                closest_coll.bounds.min.y,
                                                closest_coll.transform.position.z); //set gob flat at bottom of closest_coll
            starting_pos = transform.position;
        }
    }

    private void Update()
    {
        Debug.Log(coll.bounds.extents.y);
    }
}