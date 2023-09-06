using System;
using UnityEngine;
using UnityEngine.UI;

namespace HackingSystem {
    public class Lock : MonoBehaviour, IHackable {

        private Toggle _toggle;
        [SerializeField] private GameObject uiPrefab;
        public event Action<bool> OnValueChange;

        public void Toggle(bool value) {
            OnValueChange?.Invoke(value);
        }

        public GameObject GetUI() {
            GameObject o = Instantiate(uiPrefab);
            Toggle toggle = o.GetComponentInChildren<Toggle>();
            toggle.onValueChanged.AddListener(Toggle);
            return o;
        }
    }
}
