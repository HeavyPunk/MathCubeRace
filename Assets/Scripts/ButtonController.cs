using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject player;
    public void Open()
    {
        gameObject.SetActive(true);

        if (gameObject.transform.name.Equals("BookHolder"))
        {
            player.SetActive(false);
            gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = -3f;
        }
    }

    public void Close()
    {
        player.SetActive(true);
        gameObject.SetActive(false);
    }
}
