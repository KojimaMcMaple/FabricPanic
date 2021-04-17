using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KH_ObjectPlacementSlotController : MonoBehaviour
{
    private bool is_empty_ = true;
    private Transform parent_transform_ = null;

    private void Start()
    {
        parent_transform_ = transform.parent.parent;
    }

    public bool IsEmpty()
    {
        return is_empty_;
    }

    public void SetEmpty(bool value)
    {
        is_empty_ = value;
    }

    public Transform GetParent()
    {
        return parent_transform_;
    }
}
