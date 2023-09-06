using System;
using System.Collections.Generic;
using System.Linq;
using RobotSystem;
using UnityEngine;

namespace HackingSystem {
    //goes on camera
    public class HackableObjectDetector : MonoBehaviour {

        [SerializeField] private float coneDegrees = 30f;

        [SerializeField] private float rangeOfScanner = 20;
        private float _rangeSquared;
        
        private List<HackableObject> _hackableObjects;
        private HackableObject _focusedHackableObject;


        [SerializeField] private Camera robotCamera;

        

        private void Start() {
            HackableObject[] hackableObjects = GameObject.FindObjectsOfType<HackableObject>();
            _hackableObjects = hackableObjects.ToList();
            _rangeSquared = rangeOfScanner * rangeOfScanner;
            
        }

        private void Update() {
            List<HackableObject> objectsToHighlight = new List<HackableObject>();
            
            
            HackableObject focusedObject = null;
            float closetHackableDotProduct = 0;
            Vector3 position = transform.position;
            foreach (HackableObject hackableObject in _hackableObjects) {
                if (Vector3.SqrMagnitude(hackableObject.transform.position - position) > _rangeSquared) {
                    hackableObject.State = HackableObject.HackableState.OutOfRange;
                    hackableObject.GetComponent<MeshRenderer>().material.SetFloat("_Strength", 0.3f);
                    hackableObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);

                    continue;
                }
                
                objectsToHighlight.Add(hackableObject);
                
                float cone = Mathf.Cos(coneDegrees * Mathf.Deg2Rad);
                Vector3 heading = (hackableObject.transform.position - transform.position).normalized;

                float dot = Vector3.Dot(transform.forward, heading);
                if (dot > cone) {
                    if (dot > closetHackableDotProduct) {
                        focusedObject = hackableObject;
                        closetHackableDotProduct = dot;
                    }
                }
            }

            foreach (HackableObject o in objectsToHighlight) {
                if(o == focusedObject) continue;

                o.State = HackableObject.HackableState.InRange;
            }

            if (focusedObject != null) {
                focusedObject.State = HackableObject.HackableState.Focused;
            }

        }


    }
}
