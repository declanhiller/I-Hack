using System;
using UnityEngine;
using UnityEngine.UI;

namespace HackingSystem {
    public class HackableLock : MonoBehaviour {

        [SerializeField] private Usable door;
        [SerializeField] private GameObject uiPrefab;
        
        [SerializeField] private bool tutorialEnabled;
        [SerializeField] private GameObject tutorialPrompt;
        [SerializeField] private GameObject cameraTutorialPrompt;

        private bool _shouldPromptTrigger;

        private void Start()
        {
            if (!tutorialEnabled) return;
            Targetable targetableComponent = GetComponent<Targetable>();
            targetableComponent.OnStateChange += state =>
            {
                _shouldPromptTrigger = state == Targetable.TargetableState.Focused;
            };
        }

        private void Update()
        {
            if (!tutorialEnabled) return;
            if (cameraTutorialPrompt.activeInHierarchy) return;
            tutorialPrompt.SetActive(_shouldPromptTrigger);
        }

        public GameObject CreateUI()
        {
            tutorialEnabled = false;
            tutorialPrompt.SetActive(false);
            GameObject ui = Instantiate(uiPrefab);
            Toggle toggle = ui.GetComponentInChildren<Toggle>();
            toggle.SetIsOnWithoutNotify(door.IsOpen);
            toggle.onValueChanged.AddListener(value => door.Use(value));
            return ui;
        }
        
    }
}
