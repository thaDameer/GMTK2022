using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputUIView : MonoBehaviour
{
    [SerializeField] private Image leftTab, rightTab;
    [SerializeField] private TMP_Text leftText, rightText;
    
    
    void Awake()
    {
        CubeController.OnDiceSideChanged += UpdatedDiceView;
    }

    private void OnDestroy()
    {
        CubeController.OnDiceSideChanged -= UpdatedDiceView;    
    }

    private void UpdatedDiceView(DiceSide left, DiceSide right)
    {
        leftText.text = left.Number.ToString();
        rightText.text = right.Number.ToString();
    }
    
}
