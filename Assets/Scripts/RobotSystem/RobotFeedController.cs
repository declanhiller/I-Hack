using System;
using UnityEngine;
using UnityEngine.UI;

public class RobotFeedController : MonoBehaviour {
    [SerializeField] private Camera camera;
    private RawImage _rawImage;
    private RectTransform _rectTransform;
    private void Start() {
        // _rectTransform = GetComponent<RectTransform>();
        // _rawImage = GetComponent<RawImage>();
        // Invoke(nameof(SetRenderTexture), 0.01f);
    }

    private void SetRenderTexture() {
        
        int width = (int) (_rectTransform.rect.width);
        int height = (int) (_rectTransform.rect.height);

        RenderTexture renderTexture = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();
        
        camera.targetTexture = renderTexture;
        _rawImage.texture = renderTexture;
        
        renderTexture.Release();
    }
}