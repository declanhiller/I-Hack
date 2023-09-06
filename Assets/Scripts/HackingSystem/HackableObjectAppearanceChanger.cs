using System;
using UnityEngine;

namespace HackingSystem {
    public class HackableObjectAppearanceChanger : MonoBehaviour {

        [SerializeField] private Color focusedColor = Color.red;
        [SerializeField] private float focusedEmissionStrength;
        
        [SerializeField] private Color inRangeColor = Color.red;
        // [SerializeField] private float
        
        [SerializeField] private Color outOfRangeColor = Color.red;
        
        
        private void Start() {
            HackableObject.OnHackableStateChange += ChangeHackableObjectAppearance;
        }

        public void ChangeHackableObjectAppearance(HackableObject hackableObject, HackableObject.HackableState state) {
            switch (state) {
               case HackableObject.HackableState.Focused:
                   hackableObject.GetComponent<MeshRenderer>().material.SetFloat("_Strength", 1f);
                   // hackableObject.GetComponent<MeshRenderer>().material.SetColor("_Color", washedOutColor);
                   break;
               case HackableObject.HackableState.InRange:
                   break;
               case HackableObject.HackableState.OutOfRange:
                   break;
            }
        }
        
    }
}
