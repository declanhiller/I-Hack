using System;
using System.Collections;
using System.Collections.Generic;
using HackingSystem;
using UnityEngine;

public class CameraCone : Usable
{

    [SerializeField] private Transform camera;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;


    private void Start()
    {
        IsOpen = true;
    }

    
    private void Update()
    {
        transform.position = camera.position;
        transform.forward = -camera.right;
        if (!_isInSecurityCone) return;
        if (!IsOpen) return;
        _isInSecurityCone = false;
        player.transform.position = spawnPoint.transform.position;
    }

    private bool _isInSecurityCone;
    
    private void OnTriggerEnter(Collider other)
    {
        _isInSecurityCone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isInSecurityCone = false;
    }

    public override void Use(bool value)
    {
        IsOpen = value;
        GetComponent<MeshRenderer>().enabled = value;
    }
}
