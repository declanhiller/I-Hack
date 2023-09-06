using System;
using RobotSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace HackingSystem {
    public class HackingController : MonoBehaviour {

        [SerializeField] private InputController inputController;

        private Keybinds _keybinds;

        public HackableObject focusedObject;

        private bool _isHacking;
        

        private void Start() {
            HackableObject.OnHackableStateChange += NewFocusedObject;
            _keybinds = inputController.GetComponent<Keybinds>();
            _keybinds.Player.Hack.performed += Hack;

        }

        public void NewFocusedObject(HackableObject hackableObject, HackableObject.HackableState newState) {
            if (hackableObject.State == HackableObject.HackableState.Focused) {
                focusedObject = null;
                return;
            }

            if (newState != HackableObject.HackableState.Focused) return;
            focusedObject = hackableObject;
        }

        public void Hack(InputAction.CallbackContext context) {
            if (_isHacking) { //turn off hacking menu
                _isHacking = false;
            }
            else { //turn on hacking menu if object is focused
                if (focusedObject == null) return;
                focusedObject.Hack();
            }
            
        }


    }
}
