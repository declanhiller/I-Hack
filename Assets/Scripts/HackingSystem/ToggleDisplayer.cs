using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDisplayer : MonoBehaviour
{


    [SerializeField] private Toggle toggle;

    [SerializeField] private Color selectedColor = Color.red;
    [SerializeField] private Color notSelectedColor = Color.blue;

    [SerializeField] private Image background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private RectTransform selectedPosition;
    [SerializeField] private RectTransform notSelectedPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggle);
        background.color = toggle.isOn ? selectedColor : notSelectedColor;
        handle.position = toggle.isOn ? selectedPosition.position : notSelectedPosition.position;
    }

    private void OnToggle(bool newValue)
    {
        background.color = newValue ? selectedColor : notSelectedColor;
        handle.position = newValue ? selectedPosition.position : notSelectedPosition.position;
    }
    
}
