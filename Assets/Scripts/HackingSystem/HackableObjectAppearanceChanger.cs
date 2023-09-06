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
            Targetable.OnAnyTargetableStateChange += ChangeTargetableObjectAppearance;
        }

        public void ChangeTargetableObjectAppearance(Targetable targetable, Targetable.TargetableState state) {
            switch (state) {
               case Targetable.TargetableState.Focused:
                   targetable.GetComponent<MeshRenderer>().material.SetFloat("_Strength", 1f);
                   // hackableObject.GetComponent<MeshRenderer>().material.SetColor("_Color", washedOutColor);
                   break;
               case Targetable.TargetableState.InRange:
                   break;
               case Targetable.TargetableState.OutOfRange:
                   break;
            }
        }
        
    }
}
