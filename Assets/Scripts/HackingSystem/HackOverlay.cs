using System;
using UnityEngine;

namespace HackingSystem {
    public class HackOverlay : MonoBehaviour {


        [SerializeField] private Camera robotCamera;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private LineRenderer lineRenderer;

        [SerializeField] private float offset = -5;
        
        public Transform TrackingLocation { private get; set; }

        private void Start() {
            
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
            
            // //check if bottom is available
            // Vector3 proposedScreenPoint = robotCamera.WorldToScreenPoint(proposedWorldPoint);
            //
            // if (proposedScreenPoint.y -= overlayRect.y) {
            //     
            // }
            
            
            
            
            return proposedWorldPoint;
        }

    }
}
