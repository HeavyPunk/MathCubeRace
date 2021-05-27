using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "New Skin", menuName = "Create New Skin")]
public class SkinInfo : ScriptableObject
{
    public enum SkinID 
    {
        RubiksCube,
        PortalCube,
        CreeperCube,
        LegoCube,
        SpiderManCube,
        IronManCube,
        CaptainAmericaCube,
        HulkCube
    }

    public SkinID skinID;

    public Sprite skinSprite;

    public int skinPrice;
}
