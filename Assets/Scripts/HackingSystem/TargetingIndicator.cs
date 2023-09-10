using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace HackingSystem {
    public class TargetingIndicator : MonoBehaviour {

        [FormerlySerializedAs("focusedObject")]
        [FormerlySerializedAs("focusedStateContainer")]
        [FormerlySerializedAs("focusedObjectStateMachine")]
        public Targetable focused;
        private MeshFilter _focusedObjectMeshFilter;
        
        private RectTransform _targetIndicator;

        [SerializeField] private Camera robotCamera;

        [SerializeField] private RectTransform tl;
        [SerializeField] private RectTransform tr;
        [SerializeField] private RectTransform bl;
        [SerializeField] private RectTransform br;

        public bool Locked {
            get => _locked;
            set {
                _locked = value;
                if (!_locked) {
                    focused = _storedTargetable;
                    if (focused == null) return;
                    _focusedObjectMeshFilter = focused.GetComponent<MeshFilter>();
                    if (_focusedObjectMeshFilter == null)
                    {
                        _focusedObjectMeshFilter = focused.GetComponentInChildren<MeshFilter>();
                    }
                }
                else
                {
                    _storedTargetable = focused;
                }
            }
        }

        private bool _locked;

        //for if a targetable changes while indicator is locked, makes it so that when we unlock we can set a targetable
        private Targetable _storedTargetable;


        private void Start() {
            Targetable.OnAnyTargetableStateChange += NewTargetableObject;
            _targetIndicator = GetComponent<RectTransform>();
            _targetIndicator.gameObject.SetActive(false);
        }

        private void Update() {
            if (focused == null) {
                return;
            }

            MatchIndicatorToBox();

        }

        public void NewTargetableObject(Targetable targetable, Targetable.TargetableState newState) {
            if (Locked) {

                if (targetable.State == Targetable.TargetableState.Focused) {
                    _storedTargetable = null;
                }

                if (newState == Targetable.TargetableState.Focused) {
                    _storedTargetable = targetable;
                }
                
                return;
            }
            
            if (targetable.State == Targetable.TargetableState.Focused) {
                focused = null;
                _focusedObjectMeshFilter = null;
                _targetIndicator.gameObject.SetActive(false);
                return;
            }

            if (newState != Targetable.TargetableState.Focused) return;

            focused = targetable;
            _focusedObjectMeshFilter = targetable.GetComponent<MeshFilter>();
            if (_focusedObjectMeshFilter == null)
            {
                _focusedObjectMeshFilter = focused.GetComponentInChildren<MeshFilter>();
            }
            _targetIndicator.gameObject.SetActive(true);
            MatchIndicatorToBox();
        }

        public Rect ScreenSpaceBoundingBox() {
            Matrix4x4 transformLocalToWorldMatrix = focused.transform.localToWorldMatrix;
            Vector3[] meshVertices = _focusedObjectMeshFilter.mesh.vertices;
            Vector3 scale = _focusedObjectMeshFilter.transform.localScale;
            IEnumerable<Vector3> screenSpaceVertices = meshVertices
                .Select(v => robotCamera.WorldToScreenPoint(
                    transformLocalToWorldMatrix.MultiplyPoint3x4(new Vector3(v.x * scale.x, v.y * scale.y, v.z * scale.z))));

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
