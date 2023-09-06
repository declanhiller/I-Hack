using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HackingSystem {
    public class Targetable : MonoBehaviour {

        public static event Action<Targetable, TargetableState> OnAnyTargetableStateChange;

        public event Action<TargetableState> OnStateChange;
        
        public TargetableState State {
            set {
                if (value == _currentState) return;
                OnStateChange?.Invoke(value);
                OnAnyTargetableStateChange?.Invoke(this, value);
                _currentState = value;
            }

            get => _currentState;
        }
        
        

        private TargetableState _currentState;

        public enum TargetableState {
            Focused,
            InCone,
            InRange,
            OutOfRange
        }
        

    }
}