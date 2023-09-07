using UnityEngine;
using UnityEngine.UI;

namespace HackingSystem {
    public class HackableLock : MonoBehaviour {

        [SerializeField] private Door door;
        [SerializeField] private GameObject uiPrefab;

        public GameObject CreateUI() {
            GameObject ui = Instantiate(uiPrefab);
            Toggle toggle = ui.GetComponentInChildren<Toggle>();
            toggle.SetIsOnWithoutNotify(door.IsOpen);
            toggle.onValueChanged.AddListener(value => door.UseDoor(value));
            return ui;
        }
    }
}
