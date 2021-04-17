using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushRequestBtn_Controller : MonoBehaviour
{
    
    public GameObject rr_prefab_;
    
    private void Awake()
    {
        //rr_prefab_ = GameObject.Find("RushRequest");
    }

    public void LaunchRushRequest()
    {
        rr_prefab_.SetActive(true);
    }
}
