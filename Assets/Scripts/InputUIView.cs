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
    
    
    void Start()
    {
        CubeController.OnDiceSideChanged += UpdatedDiceView;
    }

    private void OnDestroy()
    {
        CubeController.OnDiceSideChanged -= UpdatedDiceView;    
    }

    private void UpdatedDiceView(DiceSide left, DiceSide right)
    {
        leftText.text = left.Number;
        rightText.text = right.Number;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
