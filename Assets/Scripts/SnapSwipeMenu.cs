using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapSwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    
    private float _scrollPos = 0;
    private float[] _pos;
    
    void Update()
    {
        _pos = new float[transform.childCount];
        float distance = 1f / (_pos.Length - 1f);
        
        for (int i = 0; i < _pos.Length; i++)
            _pos[i] = distance * i;
        
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            _scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < _pos.Length; i++)
            {
                Debug.Log(i + ":" + _scrollPos + ":" + (_pos[i] + (distance / 2)) + ":" + (_pos[i] - (distance / 2)));
                if (_scrollPos < _pos[i] + (distance / 2) && _scrollPos > _pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, _pos[i], 0.1f);
                }
            }
            
            //if (_scrollPos > -1 && )
        }
    }
}