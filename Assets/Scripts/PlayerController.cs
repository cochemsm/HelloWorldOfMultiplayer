using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour {
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _mouseSensitivity = 1f;
    [SerializeField] private Transform _body;
    private Rigidbody _rigidbody;
    private Camera _camera;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start() {
        if (!isLocalPlayer) return;
        
        GameObject.FindGameObjectWithTag("MainCamera").transform.SetParent(transform);
        _camera = GetComponentInChildren<Camera>();
        _camera.transform.position = new Vector3(0, 1.5f, 0);
    }

    private void Update() {
        if (!isLocalPlayer) return;
        
        _rigidbody.velocity = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * _movementSpeed);
        transform.RotateAround(_body.position, Vector3.up, Input.GetAxis("Mouse X") * _mouseSensitivity);
        _camera.transform.eulerAngles -= new Vector3(Input.GetAxis("Mouse Y") * _mouseSensitivity, 0, 0);
    }
}
