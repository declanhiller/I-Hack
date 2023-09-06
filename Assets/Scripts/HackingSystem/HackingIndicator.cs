using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace HackingSystem {
    public class HackingIndicator : MonoBehaviour {

        [FormerlySerializedAs("focusedStateContainer")] [FormerlySerializedAs("focusedObjectStateMachine")] public HackableObject focusedObject;
        
        private MeshFilter _focusedObjectMeshFilter;
        private RectTransform _targetIndicator;
        
        [SerializeField] private Camera robotCamera;

        [SerializeField] private RectTransform tl;
        [SerializeField] private RectTransform tr;
        [SerializeField] private RectTransform bl;
        [SerializeField] private RectTransform br;
        
        
        private void Start() {
            HackableObject.OnHackableStateChange += NewHackableObject;
            _targetIndicator = GetComponent<RectTransform>();
            _targetIndicator.gameObject.SetActive(false);
        }

        private void Update() {
            if (focusedObject == null) {
                return;
            }

            MatchIndicatorToBox();

        }

        public void NewHackableObject(HackableObject hackableObject, HackableObject.HackableState newState) {
            if (hackableObject.State == HackableObject.HackableState.Focused) {
                focusedObject = null;
                _focusedObjectMeshFilter = null;
                _targetIndicator.gameObject.SetActive(false);
                return;
            }
            if (newState != HackableObject.HackableState.Focused) return;
            focusedObject = hackableObject;
            _focusedObjectMeshFilter = hackableObject.GetComponent<MeshFilter>();
            _targetIndicator.gameObject.SetActive(true);
            MatchIndicatorToBox();
        }
        
        public Rect ScreenSpaceBoundingBox() {
            Matrix4x4 transformLocalToWorldMatrix = focusedObject.transform.localToWorldMatrix;
            Vector3[] meshVertices = _focusedObjectMeshFilter.mesh.vertices;
            IEnumerable<Vector3> screenSpaceVertices = meshVertices
                .Select(v => robotCamera.WorldToScreenPoint(transformLocalToWorldMatrix.MultiplyPoint3x4(v)));

            float maxX = screenSpaceVertices.Max(v => v.x);
            float maxY = screenSpaceVertices.Max(v => v.y);
            float minX = screenSpaceVertices.Min(v => v.x);
            float minY = screenSpaceVertices.Min(v => v.y);

            Rect rect = new Rect(minX, minY, maxX - minX, maxY - minY);
            return rect;
        }

        public void MatchIndicatorToBox() {
            Rect screenSpaceBoundingBox = ScreenSpaceBoundingBox();
            float xMax = screenSpaceBoundingBox.xMax;
            float xMin = screenSpaceBoundingBox.xMin;
            float yMax = screenSpaceBoundingBox.yMax;
            float yMin = screenSpaceBoundingBox.yMin;
            tl.position = new Vector3(xMin, yMax);
            tr.position = new Vector3(xMax, yMax);
            bl.position = new Vector3(xMin, yMin);
            br.position = new Vector3(xMax, yMin);
        }
    }
}
