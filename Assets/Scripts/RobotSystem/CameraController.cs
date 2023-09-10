using System;
using System.Collections;
using System.Collections.Generic;
using RobotSystem;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private float cameraSpeed = 1;
    [SerializeField] private float minHorizontalClamp = -40;
    [SerializeField] private float maxHorizontalClamp = 40;
    [SerializeField] private float minVerticalClamp = -10;
    [SerializeField] private float maxVerticalClamp = 80;

    [SerializeField] private Transform cameraTransform;

    private Keybinds _keybinds;

    private float _rotationX;

    // Start is called before the first frame update
    void Start() {
        _keybinds = GetComponentInParent<InputController>().Keybinds;
    }

    // Update is called once per frame
    void Update() {
        Vector2 inputValue = _keybinds.Player.Camera.ReadValue<Vector2>();
        Vector3 eulerAngles = transform.localRotation.eulerAngles;
        float signedYAngle = eulerAngles.y;
        if (signedYAngle > 180) {
            signedYAngle -= 360;
        }


        if ((inputValue.x > 0 && signedYAngle < maxHorizontalClamp) ||
            (inputValue.x < 0 && signedYAngle > minHorizontalClamp))
        {
            transform.Rotate(0, cameraSpeed * Time.deltaTime * inputValue.x, 0);
        }

        _rotationX += cameraSpeed * Time.deltaTime * inputValue.y;

        _rotationX = Mathf.Clamp(_rotationX, minVerticalClamp, maxVerticalClamp);
        cameraTransform.localRotation = Quaternion.Euler(-_rotationX, 0, 0);

    }
}
