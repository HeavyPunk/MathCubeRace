using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    public Sprite equipedSkinUpper;
    public Sprite equipedSkinForward;
    public Sprite equipedSkinRight;
    public Sprite equipedSkinLeft;

    public Sprite equipedSkinUpperPrev;
    public Sprite equipedSkinForwardPrev;
    public Sprite equipedSkinRightPrev;
    public Sprite equipedSkinLeftPrev;

    private void Start()
    {
        switch (PlayerPrefs.GetString("spriteShow"))
        {
            case "RubiksCube":
                FillEquipSkin(0, 1, 2, 3);
                break;
            case "PortalCube":
                FillEquipSkin(4, 4, 4, 4);
                break;
            case "CreeperCube":
                FillEquipSkin(5, 5, 5, 5);
                break;
            case "SpiderManCube":
                FillEquipSkin(7, 6, 7, 7);
                break;
            case "IronManCube":
                FillEquipSkin(9, 8, 9, 9);
                break;
            case "CaptainAmericaCube":
                FillEquipSkin(11, 10, 11, 11);
                break;
            case "HulkCube":
                FillEquipSkin(13, 12, 13, 13);
                break;
            case "LegoCube":
                FillEquipSkin(14, 14, 14, 14);
                break;
        }

        ChangeSkin();
    }

    public void EquipSkin(SkinInfo skinInfo)
    {
        PlayerPrefs.SetString("spriteShow", skinInfo.skinID.ToString());

        equipedSkinUpperPrev = equipedSkinUpper;
        equipedSkinForwardPrev = equipedSkinForward;
        equipedSkinRightPrev = equipedSkinRight;
        equipedSkinLeftPrev = equipedSkinLeft;

        switch (skinInfo.skinID)
        {
            case SkinInfo.SkinID.RubiksCube:
                FillEquipSkin(0, 1, 2, 3);
                break;
            case SkinInfo.SkinID.PortalCube:
                FillEquipSkin(4, 4, 4, 4);
                break;
            case SkinInfo.SkinID.CreeperCube:
                FillEquipSkin(5, 5, 5, 5);
                break;
            case SkinInfo.SkinID.SpiderManCube:
                FillEquipSkin(7, 6, 7, 7);
                break;
            case SkinInfo.SkinID.IronManCube:
                FillEquipSkin(9, 8, 9, 9);
                break;
            case SkinInfo.SkinID.CaptainAmericaCube:
                FillEquipSkin(11, 10, 11, 11);
                break;
            case SkinInfo.SkinID.HulkCube:
                FillEquipSkin(13, 12, 13, 13);
                break;
            case SkinInfo.SkinID.LegoCube:
                FillEquipSkin(14, 14, 14, 14);
                break;

        }
    }

    void FillEquipSkin(int first, int second, int third, int fourth)
    {
        equipedSkinUpper = sprites[first];
        equipedSkinForward = sprites[second];
        equipedSkinRight = sprites[third];
        equipedSkinLeft = sprites[fourth];
    }

    public void ChangeSkin()
    { 
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = equipedSkinUpper;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = equipedSkinForward;
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = equipedSkinRight;
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = equipedSkinLeft;
    }
}
