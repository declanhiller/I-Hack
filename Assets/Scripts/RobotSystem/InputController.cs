using UnityEngine;

namespace RobotSystem {
    public class InputController : MonoBehaviour {
        public Keybinds Keybinds {
            get {
                if (_keybinds == null) {
                    _keybinds = new Keybinds();
                    _keybinds.Enable();
                }

                return _keybinds;
            }
        }

        private Keybinds _keybinds;
    }
}
