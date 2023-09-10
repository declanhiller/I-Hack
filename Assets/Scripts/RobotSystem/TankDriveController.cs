using System;
using System.Collections;
using System.Collections.Generic;
using RobotSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class TankDriveController : MonoBehaviour {

    private Keybinds _keybinds;
    private Rigidbody _rb;
    
    private float _throttle;
    
    [SerializeField] private float gravity;

    [SerializeField] private float maxThrottle = 4;
    [SerializeField] private float minThrottle = -2;
    [SerializeField] private float throttleIncrease = 0.5f;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private AudioSource driveSound;
    private bool _isPlaying;
    
    // Start is called before the first frame update

    private void Awake() {

        _keybinds = GetComponent<InputController>().Keybinds;
        _rb = GetComponent<Rigidbody>();
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
        Vector2 inputDirection = _keybinds.Player.Movement.ReadValue<Vector2>();

        if (Math.Abs(inputDirection.y) <= 0.05f && _throttle != 0) {
            _throttle = 0;
        }

        if (!_isPlaying && inputDirection.SqrMagnitude() > 0.1f)
        {
            driveSound.Play();
            driveSound.time = 0.7f;
            _isPlaying = true;
        } else if (_isPlaying && inputDirection.SqrMagnitude() < 0.1f)
        {
            driveSound.Stop();
            _isPlaying = false;
        }

        if ((inputDirection.y > 0 && _throttle <= maxThrottle) || 
            (inputDirection.y < 0 && _throttle >= minThrottle)) {
            _throttle += inputDirection.y * throttleIncrease;
        }

        _rb.velocity = transform.forward * _throttle * maxThrottle + new Vector3(0, _rb.velocity.y, 0);
        
        float rotation = inputDirection.x;
        if (inputDirection.y < 0) {
            rotation = -rotation;
        }

        transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);


    }
}
