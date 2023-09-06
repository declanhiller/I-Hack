using System;
using RobotSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace HackingSystem {
    public class HackingController : MonoBehaviour {

        [SerializeField] private InputController inputController;
        [SerializeField] private TargetableDetector targetableDetector;
        [SerializeField] private TargetingIndicator targetableIndicator;

        private Keybinds _keybinds;
        
        private bool _isHacking;

        private Targetable _currentTargetable;
        private HackableLock _currentHackable;

        [SerializeField] private HackOverlay hackOverlay;
        

        private void Start() {
            Targetable.OnAnyTargetableStateChange += NewFocusedObject;
            _keybinds = inputController.Keybinds;
            _keybinds.Player.Hack.performed += Hack;

        }

        public void NewFocusedObject(Targetable targetable, Targetable.TargetableState newState) {
            if (targetable.State == Targetable.TargetableState.Focused) { //switched off of focused
                if (_currentTargetable != null) {
                    _currentTargetable.OnStateChange -= OnTargetableStateChange;
                }
                _currentTargetable = null;
                _currentHackable = null;
                return;
            }

            if (newState != Targetable.TargetableState.Focused) return;
            if (_currentTargetable != null) {
                _currentTargetable.OnStateChange -= OnTargetableStateChange;
            }
            _currentHackable = targetable.GetComponent<HackableLock>();
            _currentTargetable = targetable;
            _currentTargetable.OnStateChange += OnTargetableStateChange;
        }

        public void OnTargetableStateChange(Targetable.TargetableState targetableState) {
            if (targetableState != Targetable.TargetableState.OutOfRange &&
                targetableState != Targetable.TargetableState.InRange) return;
            ActivateOverlay(false);
        }

        public void Hack(InputAction.CallbackContext context) {
            if (_isHacking) { //turn off hacking menu
                ActivateOverlay(false);
            }
            else { //turn on hacking menu if object is focused
                if (_currentHackable == null) return;
                ActivateOverlay(true);
            }
            
        }

        public void ActivateOverlay(bool active) {
            if (active) {
                hackOverlay.gameObject.SetActive(true);
                hackOverlay.TrackingLocation = _currentHackable.gameObject.transform;
                _isHacking = true;
                targetableIndicator.Locked = true;
            }
            else {
                hackOverlay.TrackingLocation = null;
                hackOverlay.gameObject.SetActive(false);
                _isHacking = false;
                targetableIndicator.Locked = false;
            }
        }


    }
}
