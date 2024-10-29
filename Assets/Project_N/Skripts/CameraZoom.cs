using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    private Vector3 _position;





// Update is called once per frame
    void Update()
    {
        _position = _cam.position;
        if (Input.GetKey(KeyCode.R))
        {
            _position.z -= 1;
            _cam.position = _position;
        }

    }
}


