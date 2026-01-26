using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
   [SerializeField] private CameraOrbit _cameraOrbit;
  // [SerializeField] private Button _button;
  [SerializeField] private GameObject _buttonGameObject;
  [SerializeField] private Button _button;
  [SerializeField] private TextMeshProUGUI _textMesh;
  [SerializeField] private int _moveSpeed = 1;
  [SerializeField] private Transform _cam;
  private Rigidbody _rb;
  private 
  
    void Start()
    {
        if (_cameraOrbit != null)
        {
            _cameraOrbit = this.GetComponent<CameraOrbit>();
        }

        _button = _buttonGameObject.GetComponent<Button>();
        _button.onClick.AddListener(ChangeCameraControlType);
        
        _rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_cameraOrbit.LockMove)
        {
            
            _cam = Camera.main.transform;

            float horInput = Input.GetAxisRaw("Horizontal") * _moveSpeed;
            float verInput = Input.GetAxisRaw("Vertical") * _moveSpeed;
    
            //camera direction

            Vector3 camForward = _cam.forward;
            Vector3 camRight = _cam.right;

            camForward.y = 0;
            camRight.y = 0;
    
            //creating relate camera direction
            Vector3 forvardRelative = verInput * camForward;
            Vector3 rightRelative = horInput * camRight;

            Vector3 moveDir = forvardRelative + rightRelative;
            
            _rb.velocity = new Vector3(moveDir.x, _rb.velocity.y, moveDir.z);
            
        }
    }

    public void ChangeCameraControlType()
    {
        _cameraOrbit.LockMove = !_cameraOrbit.LockMove;
        if (_cameraOrbit.LockMove)
        {
            _textMesh.text = $"Lock On Character";
        }
        else
        {
            _textMesh.text = $"Free Movement";
        }

        
        
    }
    
}
