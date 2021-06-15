using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MathActions : MonoBehaviour
{
    public Sprite notActiveSprite;
    public Sprite activeSprite;
    
    private char[] _availableActions = new char[4] { '0', '0', '0', '0'};
    private char _currentAction;
    void Start()
    {
        FirstCheckOperators();
        FillAvailableActions();
        
        _currentAction = transform.GetChild(0).GetComponent<Text>().text.ToCharArray()[0];
        if (_availableActions.Contains(_currentAction))
        {
            transform.GetComponent<Image>().sprite = activeSprite;
        }
        else
        {
            transform.GetComponent<Image>().sprite = notActiveSprite;
        }
    }
    
    void Update()
    {
        FillAvailableActions();
    }

    public void OnMathActionButton()
    {
        if (_availableActions.Contains(_currentAction))
        {
            if (CountOfOperators() > 1)
            {
                transform.GetComponent<Image>().sprite = notActiveSprite;
                SetOperators(_currentAction, true);
            }
        }
        else
        {
            transform.GetComponent<Image>().sprite = activeSprite;
            SetOperators(_currentAction, false);
        }
    }

    private void SetOperators(char currentAction, bool contains)
    {
        switch (currentAction)
        {
            case '+':
                PlayerPrefs.SetString("OperatorPlus", contains ? "0" : "+");
                break;
            case '-':
                PlayerPrefs.SetString("OperatorMinus", contains ? "0" : "-");
                break;
            case 'x':
                PlayerPrefs.SetString("OperatorMultiply", contains ? "0" : "x");
                break;
            case '÷':
                PlayerPrefs.SetString("OperatorDivide", contains ? "0" : "÷");
                break;
        }
    }

    private void FillAvailableActions()
    {
        _availableActions[0] = PlayerPrefs.GetString("OperatorPlus", "0")[0];
        _availableActions[1] = PlayerPrefs.GetString("OperatorMinus", "0")[0];
        _availableActions[2] = PlayerPrefs.GetString("OperatorMultiply", "0")[0];
        _availableActions[3] = PlayerPrefs.GetString("OperatorDivide", "0")[0];
    }

    public static void FirstCheckOperators()
    {
        if (!PlayerPrefs.HasKey("OperatorPlus")
            && !PlayerPrefs.HasKey("OperatorMinus")
            && !PlayerPrefs.HasKey("OperatorMultiply")
            && !PlayerPrefs.HasKey("OperatorDivide"))
        {
            PlayerPrefs.SetString("OperatorPlus", "+");
            PlayerPrefs.SetString("OperatorMinus", "-");
            PlayerPrefs.SetString("OperatorMultiply", "x");
            PlayerPrefs.SetString("OperatorDivide", "÷");
        }
    }

    private int CountOfOperators()
    {
        int count = 0;
        foreach (var action in _availableActions)
        {
            if (action.Equals('0') == false)
                count++;
        }
        return count;
    }
}
