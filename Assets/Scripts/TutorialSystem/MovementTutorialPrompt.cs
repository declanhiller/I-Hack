using System;
using UnityEngine;

namespace TutorialSystem
{
    public class MovementTutorialPrompt : MonoBehaviour
    {

        [SerializeField] private GameObject cameraTutorialPrompt;
        [SerializeField] private GameObject movementTutorialPrompt;

        private bool _waitingForMovementPromptToGo;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_waitingForMovementPromptToGo) return;
            // movementTutorialPrompt.SetActive(false);
            // gameObject.SetActive(false);
            _waitingForMovementPromptToGo = true;
        }

        private void Update()
        {
            if (!_waitingForMovementPromptToGo) return;
            if (movementTutorialPrompt.activeInHierarchy) return;
            movementTutorialPrompt.SetActive(false);
            gameObject.SetActive(false);
            cameraTutorialPrompt.SetActive(true);

        }
    }
}