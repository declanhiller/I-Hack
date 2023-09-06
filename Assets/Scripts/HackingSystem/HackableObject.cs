using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HackingSystem {
    public class HackableObject : MonoBehaviour {

        public static event Action<HackableObject, HackableState> OnHackableStateChange;
        private MeshFilter _meshFilter;
        
        public HackableState State {
            set {
                if (value == _currentState) return;
                Debug.Log(value);
                OnHackableStateChange?.Invoke(this, value);
                _currentState = value;
            }

            get { return _currentState; }
        }

        private HackableState _currentState;

        public enum HackableState {
            Focused,
            InRange,
            OutOfRange
        }

        private void Start() {
            _meshFilter = GetComponent<MeshFilter>();
        }

        private void Update() {
        }

        public void Hack() {
            // _currentHackUI = Instantiate(hackUIPrefab, _canvas.transform);
            // _currentHackUIRectTransform = _currentHackUI.GetComponent<RectTransform>();
            // Toggle toggle = _currentHackUI.GetComponentInChildren<Toggle>();
            // toggle.onValueChanged.AddListener(value => {
            //     if (value) {
            //         door.OpenDoor();
            //     }
            //     else {
            //         door.CloseDoor();
            //     }
            // });
            
        }

        public void StopHacking() {
            // Toggle toggle = _currentHackUI.GetComponentInChildren<Toggle>();
            // toggle.onValueChanged.RemoveAllListeners();
        }

    }
}
