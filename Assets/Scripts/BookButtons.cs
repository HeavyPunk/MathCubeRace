using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookButtons : MonoBehaviour
{
    public GameObject PagePlus;
    public GameObject PlusOneDigitPage;
    public GameObject PlusManyDigitPage;

    public GameObject PageMinus;
    public GameObject MinusOneDigitPage;
    public GameObject MinusManyDigitPage;
    
    public GameObject PageMultiply;
    public GameObject MultiplyOneDigitPage;
    public GameObject MultiplyTwoDigitPage;
    public GameObject MultiplyOn4Page;
    public GameObject MultiplyOn5Page;
    public GameObject MultiplyOn9Page;
    public GameObject MultiplyOn11Page;

    public GameObject PageDivide;
    public GameObject DivideOneDigitPage;
    public GameObject DivideTwoDigitPage;

    public void Start()
    {
        PlusManyDigitPage.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = 0f;
        MinusManyDigitPage.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = 0f;
        MultiplyOneDigitPage.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = 0f;
        MultiplyTwoDigitPage.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = 0f;
    }

    public void OnPlusOneDigitButton()
    {
        PagePlus.SetActive(true);
        PlusOneDigitPage.SetActive(true);
    }
    
    public void OnPlusManyDigitButton()
    {
        PagePlus.SetActive(true);
        PlusManyDigitPage.SetActive(true);
        PlusManyDigitPage.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = -3f;
    }

    public void OnPlusCloseButton()
    {
        PlusOneDigitPage.SetActive(false);
        PlusManyDigitPage.SetActive(false);
        PagePlus.SetActive(false);
    }

    public void OnMinusOneDigitButton()
    {
        PageMinus.SetActive(true);
        MinusOneDigitPage.SetActive(true);
    }
    
    public void OnMinusManyDigitButton()
    {
        PageMinus.SetActive(true);
        MinusManyDigitPage.SetActive(true);
        MinusManyDigitPage.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = -3f;
    }
    
    public void OnMinusCloseButton()
    {
        MinusOneDigitPage.SetActive(false);
        MinusManyDigitPage.SetActive(false);
        PageMinus.SetActive(false);
    }

    public void OnMultiplyOneDigitButton()
    {
        PageMultiply.SetActive(true);
        MultiplyOneDigitPage.SetActive(true);
        MultiplyOneDigitPage.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = -3f;
    }
    
    public void OnMultiplyTwoDigitButton()
    {
        PageMultiply.SetActive(true);
        MultiplyTwoDigitPage.SetActive(true);
        MultiplyTwoDigitPage.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = -3f;
    }
    
    public void OnMultiplyOn4Button()
    {
        PageMultiply.SetActive(true);
        MultiplyOn4Page.SetActive(true);
    }
    
    public void OnMultiplyOn5Button()
    {
        PageMultiply.SetActive(true);
        MultiplyOn5Page.SetActive(true);
    }
    
    public void OnMultiplyOn9Button()
    {
        PageMultiply.SetActive(true);
        MultiplyOn9Page.SetActive(true);
    }
    
    public void OnMultiplyOn11Button()
    {
        PageMultiply.SetActive(true);
        MultiplyOn11Page.SetActive(true);
    }

    public void OnMultiplyCloseButton()
    {
        MultiplyOneDigitPage.SetActive(false);
        MultiplyTwoDigitPage.SetActive(false);
        MultiplyOn4Page.SetActive(false);
        MultiplyOn5Page.SetActive(false);
        MultiplyOn9Page.SetActive(false);
        MultiplyOn11Page.SetActive(false);
        PageMultiply.SetActive(false);
    }
    
    public void OnDivideOneDigitButton()
    {
        PageDivide.SetActive(true);
        DivideOneDigitPage.SetActive(true);
    }
    
    public void OnDivideTwoDigitButton()
    {
        PageDivide.SetActive(true);
        DivideTwoDigitPage.SetActive(true);
    }

    public void OnDivideCloseButton()
    {
        DivideOneDigitPage.SetActive(false);
        DivideTwoDigitPage.SetActive(false);
        PageDivide.SetActive(false);
    }
}