using System;
using System.Collections.Generic;
using System.Linq;
using RobotSystem;
using UnityEngine;

namespace HackingSystem {
    //goes on camera
    public class TargetableDetector : MonoBehaviour {

        [SerializeField] private float coneDegrees = 30f;

        [SerializeField] private float rangeOfScanner = 20;
        private float _rangeSquared;
        
        private List<Targetable> _targetableObjects;
        private Targetable _focusedTargetable;

        [SerializeField] private Camera robotCamera;

        
        private void Start() {
            Targetable[] hackableObjects = GameObject.FindObjectsOfType<Targetable>();
            _targetableObjects = hackableObjects.ToList();
            _rangeSquared = rangeOfScanner * rangeOfScanner;
            
        }

        private void Update() {
            List<Targetable> objectsInCone = new List<Targetable>();
            
            Targetable focused = null;
            float closetTargetableDotProduct = 0;
            Vector3 position = transform.position;
            foreach (Targetable targetableObject in _targetableObjects) {
                if (Vector3.SqrMagnitude(targetableObject.transform.position - position) > _rangeSquared) {
                    targetableObject.State = Targetable.TargetableState.OutOfRange;
                    continue;
                }
                
                float cone = Mathf.Cos(coneDegrees * Mathf.Deg2Rad);
                Vector3 heading = (targetableObject.transform.position - transform.position).normalized;

                float dot = Vector3.Dot(transform.forward, heading);
                if (dot > cone) {
                    objectsInCone.Add(targetableObject);
                    if (dot > closetTargetableDotProduct) {
                        focused = targetableObject;
                        closetTargetableDotProduct = dot;
                    }
                }
                else {
                    targetableObject.State = Targetable.TargetableState.InRange;
                }
            }

            foreach (Targetable o in objectsInCone) {
                if(o == focused) continue;
                o.State = Targetable.TargetableState.InCone;
            }

            if (focused != null) {
                focused.State = Targetable.TargetableState.Focused;
            }

        }
        
    }
}
