using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreserveSquare : MonoBehaviour {
    private LayoutElement _layoutElement;

    private RectTransform _rectTransform;
    // Start is called before the first frame update
    void Start() {
        _layoutElement = GetComponent<LayoutElement>();
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        _layoutElement.preferredWidth = _rectTransform.rect.height;
    }
}
