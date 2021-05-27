using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinInShop : MonoBehaviour
{
    public SkinInfo skinInfo;

    public Button skinButton;
    public Image skinMask;

    public bool isSkinUnlocked;

    public void Awake()
    {
        skinButton.GetComponent<Image>().sprite = skinInfo.skinSprite;
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = skinInfo.skinPrice.ToString();
        CheckSkinUnlock();
        CheckSkinActive();
    }

    public void CheckSkinUnlock()
    {
        if (PlayerPrefs.GetInt(skinInfo.skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void CheckSkinActive()
    {
        if (PlayerPrefs.GetString(skinInfo.skinID + "_Active").Equals("Yes"))
        {
            transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    public void OnButtonPress()
    {
        if (isSkinUnlocked)
        {
            CheckOnlyOneActiveSkin();
            PlayerPrefs.SetString(skinInfo.skinID + "_Active", "Yes");
            transform.GetChild(0).GetComponent<Image>().enabled = true;
            FindObjectOfType<SkinManager>().EquipSkin(skinInfo);
        }
        else
        {
            PlayerPrefs.SetString(skinInfo.skinID + "_Active", "No");
            if (FindObjectOfType<PlayerCoins>().TryBuySkin(skinInfo.skinPrice))
            {
                isSkinUnlocked = true;
                PlayerPrefs.SetInt(skinInfo.skinID.ToString(), 1);
                skinMask.gameObject.SetActive(false);
                CheckSkinUnlock();
            }
        }
    }

    public void CheckOnlyOneActiveSkin()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
                if (PlayerPrefs.HasKey(transform.parent.GetChild(i).GetComponent<SkinInShop>().skinInfo.skinID + "_Active"))
                {
                    PlayerPrefs.DeleteKey(transform.parent.GetChild(i).GetComponent<SkinInShop>().skinInfo.skinID + "_Active");
                    transform.parent.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
                }
        }
    }
}
