using System;
using UnityEngine;

namespace TutorialSystem
{
    public class MovementTutorialPrompt : MonoBehaviour
    {

        [SerializeField] private GameObject cameraTutorialPrompt;
        [SerializeField] private GameObject movementTutorialPrompt;
        
        private void OnTriggerEnter(Collider other)
        {
            cameraTutorialPrompt.SetActive(true);
            movementTutorialPrompt.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}