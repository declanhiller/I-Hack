using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulser : MonoBehaviour
{
    [SerializeField] private float visibleDuration = 0.3f;
    [SerializeField] private float invisibleDuration = 0.5f;

    [SerializeField] private Image image;
    
    private float _timer = 0;
    private bool _isVisible = true;
    
    private void Update()
    {
        if (_timer >= (_isVisible ? visibleDuration : invisibleDuration))
        {
            _isVisible = !_isVisible;
            image.enabled = _isVisible;
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }
}
