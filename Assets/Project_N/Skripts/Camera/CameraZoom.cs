using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraZoom : MonoBehaviour
{
    private Vector3 _position;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        _position = transform.localPosition;
        if (Input.GetKey(KeyCode.R))
        {
            _position.z -= 1;
            transform.localPosition = _position;
        }
            
        if (Input.GetKey(KeyCode.T))
        {
            _position.z += 1;
            transform.localPosition = _position;
        }
        
           
          
        

    }
}
