using System;
using UnityEngine;

namespace HackingSystem {
    public class HackOverlay : MonoBehaviour {


        [SerializeField] private Camera robotCamera;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private LineRenderer lineRenderer;

        [SerializeField] private float offset = -5;

        private GameObject _containedUI;
        
        public Transform TrackingLocation { private get; set; }

        private void OnDisable() {
            Destroy(_containedUI);
            _containedUI = null;
        }

        private void Update() {
            if (TrackingLocation == null) return;
            
            lineRenderer.SetPosition(0, TrackingLocation.position);
            
            Vector3 overlayPosition = CalculateOverlayWorldPosition();
            
            lineRenderer.SetPosition(1, overlayPosition);

            rectTransform.position = robotCamera.WorldToScreenPoint(overlayPosition);
        }

        private Vector3 CalculateOverlayWorldPosition() {

            Rect boundingScreenRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect;
            Rect overlayRect = rectTransform.rect;

            Vector3 proposedWorldPoint = -robotCamera.transform.up * (Vector3.Distance(robotCamera.transform.position, TrackingLocation.position) / offset) + TrackingLocation.position;
            
            
            return proposedWorldPoint;
        }

        public void SetContainedUI(GameObject ui) {
            RectTransform uiTransform = ui.GetComponent<RectTransform>();
            uiTransform.SetParent(transform);
            uiTransform.anchoredPosition = new Vector3(0, 0);
            _containedUI = ui;
        }
    }
}
