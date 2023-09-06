using System;
using System.Collections.Generic;
using System.Linq;
using HackingSystem;
using RobotSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace {
    public class Map : MonoBehaviour {

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform playerCameraTransform;

        [SerializeField] private GameObject mapMarker;
        [SerializeField] private GameObject playerMarker;
        [SerializeField] private GameObject playerViewMarker;

        private GameObject[] _thingsToTrack;

        [SerializeField] private float mapScale;

        private Dictionary<GameObject, GameObject> _mapMarkers;

        private Rect _currentWorldBoundsForMap;

        private float _width;
        private float _height;

        private void Start() {
            _mapMarkers = new Dictionary<GameObject, GameObject>();

            Invoke(nameof(SetWidthHeight), 0.1f);
            
            _thingsToTrack = GameObject.FindObjectsOfType<HackableObject>().Select(obj => obj.gameObject).ToArray();
            
            foreach (GameObject objectToTrack in _thingsToTrack) {
                GameObject marker = Instantiate(mapMarker, transform);
                _mapMarkers.Add(objectToTrack, marker);
            }
        }

        private void Update() {
            RecaclculateWorldBounds();
            RedrawMap();
        }

        private void RecaclculateWorldBounds() {
            float worldWidth = _width * mapScale;
            float worldHeight = _height * mapScale;
            Vector3 playerPosition = playerTransform.position;
            float minX = playerPosition.x - worldWidth / 2;
            float minY = playerPosition.z - worldHeight / 2;
            _currentWorldBoundsForMap = new Rect(minX, minY, worldWidth, worldHeight);
        }

        private void RedrawMap() {
            foreach (GameObject objectToTrack in _thingsToTrack) {

                GameObject marker = _mapMarkers[objectToTrack];
                if (IsPointOnMap(objectToTrack.transform.position)) {
                    if (!marker.activeInHierarchy) marker.SetActive(true);
                    Vector2 centerPosition = _currentWorldBoundsForMap.center;
                    Vector2 arrow = (new Vector2(objectToTrack.transform.position.x, objectToTrack.transform.position.z) - 
                                     new Vector2(playerTransform.position.x, playerTransform.position.z)
                        ) / mapScale;
                    Vector2 markerPosition = arrow + centerPosition;
                    marker.GetComponent<RectTransform>().anchoredPosition = markerPosition;

                }
                else {
                    if (marker.activeInHierarchy) marker.SetActive(false);
                }

            }


            playerMarker.transform.localRotation = Quaternion.Euler(0, 0,
                Vector3.SignedAngle(playerTransform.forward, Vector3.forward, Vector3.up));
            
            playerViewMarker.transform.localRotation = Quaternion.Euler(0, 0,
                Vector3.SignedAngle(playerCameraTransform.forward, Vector3.forward, Vector3.up));
        }

        private bool IsPointOnMap(Vector3 pointPosition) {
            return _currentWorldBoundsForMap.Contains(new Vector2(pointPosition.x, pointPosition.z));
        }

        void SetWidthHeight() {
            Rect rect = GetComponent<RectTransform>().rect;
            _width = rect.width;
            _height = rect.height;
        }

    }
}
