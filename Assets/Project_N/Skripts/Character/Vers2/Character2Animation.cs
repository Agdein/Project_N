using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Animation : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    [SerializeField] private float _jumpHeight = 5.0f;
    [SerializeField] private float _moveSpeed = 10.0f;

    private void OnEnable()
    {
        InputManager.EJump += Jump;
        InputManager.EMoveForvard += MoveForvard;
        InputManager.EMoveBack += MoveBack;
        InputManager.EMoveLeft += MoveLeft;
        InputManager.EMoveRight += MoveRight;
    }
    private void OnDisable()
    {
        InputManager.EJump -= Jump;
        InputManager.EMoveForvard -= MoveForvard;
        InputManager.EMoveBack -= MoveBack;
        InputManager.EMoveLeft -= MoveLeft;
        InputManager.EMoveRight -= MoveRight;
    }

    private void Jump()
    {
        if (_rigidbody.velocity.y < 1000)
        {
            _rigidbody.velocity = new Vector3( _rigidbody.velocity.x, _jumpHeight,  _rigidbody.velocity.z);
            
            
        }
    }
    private void MoveForvard()
    {
        
    }
    private void MoveBack()
    {
        
    }
    private void MoveLeft()
    {
        
    }
    private void MoveRight()
    {
        
    }

    void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        
    }
}
