﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDialogueIntro : MonoBehaviour
{
    public GameObject dialog1, dialog2, dialog3, dialog4, dialog5, dialog6, dialog7, dialog8, dialog9;
    public GameObject pointer, tuttext1, deliveryBoxA;
    private float endingCount;

    public int iState;
    private bool isEndingState;

    private bool is_skipping_dialogues = false;
    [SerializeField]
    private GameObject hud_ui_;

    

    private void Awake()
    {
        //DISABLE COMPONENTS BEFORE SCENE IS SHOWN
        pointer.SetActive(false);
        hud_ui_.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEndingState && !is_skipping_dialogues)
        {
            endingCount += Time.deltaTime;
            if(endingCount > 3) { 
                StateComplete(); 
                endingCount = 0; 
                isEndingState = false; 
            }
        }

    }

    public void StateComplete()
    {
        //if (iState == 6) return;
        iState++;
        switch (iState)
        {
            case 1:
                dialog1.SetActive(false);
                dialog2.SetActive(true);
                break;

            case 2:
                dialog2.SetActive(false);
                dialog3.SetActive(true);

                break;

            case 3:
                dialog3.SetActive(false);
                dialog4.SetActive(true);
                break;

            case 4:
                dialog4.SetActive(false);
                dialog5.SetActive(true);
                break;
            case 5: 
                dialog5.SetActive(false);
                pointer.SetActive(true);
                deliveryBoxA.GetComponent<DeliveriesBoxController>().isActivated = true;
                hud_ui_.SetActive(true);
                break;
            case 6:
                dialog6.SetActive(false);
                pointer.SetActive(true);
                break;
            case 7:
                dialog7.SetActive(false);
                dialog8.SetActive(true);
                break;
            case 8:
                dialog8.SetActive(false);
                dialog9.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void EndingState()
    {
        isEndingState = true;
    }

    public void DoSkipDialogues()
    {
        is_skipping_dialogues = true;
        dialog1.SetActive(false);
        dialog2.SetActive(false);
        dialog3.SetActive(false);
        dialog4.SetActive(false);
        dialog5.SetActive(false);
        deliveryBoxA.GetComponent<DeliveriesBoxController>().isActivated = true;
        hud_ui_.SetActive(true);
    }
}
