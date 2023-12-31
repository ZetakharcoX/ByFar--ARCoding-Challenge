using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimController : MonoBehaviour
{
    public GameObject[] colors;
    public Sprite[] openAndCloseICon;
    public Image selectedIconPlacement;
    public Vector3[] selectedColorPosition;
    public bool isColorEnable = false;
    bool isDefaultColor = true;
    Color defaultCarColor = Color.green;
    [SerializeField]
    Material primaryColor;
    Image openAndCloseButton;
    Image selectedIcon;

    private void Start()
    {
        primaryColor.color = Color.red;
        openAndCloseButton = GetComponent<Image>();
        
    }

    

    public void EnableColors()
    {
        if(!isColorEnable)
        {
            foreach (GameObject g in colors)
            {
                g.SetActive(true);
            }
            isColorEnable = true;
            openAndCloseButton.sprite = openAndCloseICon[1];
        }
        else
        {
            foreach (GameObject g in colors)
            {
                g.SetActive(false);
            }
            isColorEnable = false;
            openAndCloseButton.sprite = openAndCloseICon[0];
        }
    }

    public void ChangeRedColor()
    {
        isDefaultColor = true;
        if (isDefaultColor)
        {
            if (primaryColor.color != Color.red)
            {
                primaryColor.color = Color.red;
                selectedIconPlacement.rectTransform.localPosition = selectedColorPosition[0];
                isDefaultColor = false;
            }
            else
            {
                return;
            }

        }
    }
        
    public void ChangeBlueColor()   
    {
        isDefaultColor = true;
        if (isDefaultColor)
        {
            if (primaryColor.color != Color.blue)
            {
                primaryColor.color = Color.blue;
                selectedIconPlacement.rectTransform.localPosition = selectedColorPosition[1];
                isDefaultColor = false;
            }
        }
    }

    public void ChangeYellowColor()
    {
        isDefaultColor = true;
        if (isDefaultColor)
        {
            if (primaryColor.color != Color.yellow)
            {
                primaryColor.color = Color.yellow;
                selectedIconPlacement.rectTransform.localPosition = selectedColorPosition[2];
                isDefaultColor = false;
            }
        }
    }
    
}

