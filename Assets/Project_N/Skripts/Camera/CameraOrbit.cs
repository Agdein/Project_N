using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] public Transform _focalPoint;
    
    [SerializeField] private float _mousSpeed = 3;
    [SerializeField] private float _orbitDamping = 10;
    private Vector3 localRotarion;
    
    public bool LockMove = true;

    private void Start()
    {
       
    }


    void Update()
    {

        if (LockMove)
        {
            transform.position = _focalPoint.position;
        }
        
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
             localRotarion.x += Input.GetAxis("Mouse X") * _mousSpeed;
                    localRotarion.y -= Input.GetAxis("Mouse Y") * _mousSpeed;
            
                    localRotarion.y = Mathf.Clamp(localRotarion.y, 0f, 80f);
            
                    Quaternion QT = Quaternion.Euler(localRotarion.y, localRotarion.x, 0f);
            
                    transform.rotation = Quaternion.Lerp(transform.rotation, QT, Time.deltaTime * _orbitDamping);
        }

    }

    public void ChangeFocalPoint(Transform newTransform)
    {
        _focalPoint = newTransform;
    }
}
