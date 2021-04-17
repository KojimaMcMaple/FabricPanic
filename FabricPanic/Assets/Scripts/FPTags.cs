using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPTags
{
    public enum ObjectTag
    {
        LargeFabricBolt,
        SmallFabricBolt,
        ThreadSpool,
        ButtonBox,
        Tool
    };

    public enum FurnitureTag
    {
        Bin,
        Table,
        Shelf
    };

    public static Dictionary<ObjectTag, List<FurnitureTag>> ObjectCompatibilityDict = new Dictionary<ObjectTag, List<FurnitureTag>>()
    {
        {ObjectTag.LargeFabricBolt,  new List<FurnitureTag>{FurnitureTag.Bin, FurnitureTag.Shelf} },
        {ObjectTag.SmallFabricBolt,  new List<FurnitureTag>{FurnitureTag.Bin, FurnitureTag.Shelf} },
        {ObjectTag.ThreadSpool,  new List<FurnitureTag>{FurnitureTag.Table, FurnitureTag.Shelf} },
        {ObjectTag.ButtonBox,  new List<FurnitureTag>{FurnitureTag.Table, FurnitureTag.Shelf} },
        {ObjectTag.Tool,  new List<FurnitureTag>{FurnitureTag.Table, FurnitureTag.Shelf} }
    };

    public static bool IsObjectCompatible(ObjectTag obj_tag, FurnitureTag furn_tag)
    {
        if (ObjectCompatibilityDict[obj_tag].Contains(furn_tag))
        {
            return true;
        }
        return false;
    }
}
