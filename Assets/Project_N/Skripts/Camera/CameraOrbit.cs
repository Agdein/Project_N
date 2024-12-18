using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _mousSpeed = 3;
    [SerializeField] private float _orbitDamping = 10;
    private Vector3 localRotarion;

    void Update()
    {
        transform.position = _player.position;

        localRotarion.x += Input.GetAxis("Mouse X") * _mousSpeed;
        localRotarion.y -= Input.GetAxis("Mouse Y") * _mousSpeed;

        localRotarion.y = Mathf.Clamp(localRotarion.y, 0f, 80f);

        Quaternion QT = Quaternion.Euler(localRotarion.y, localRotarion.x, 0f);

        transform.rotation = Quaternion.Lerp(transform.rotation, QT, Time.deltaTime * _orbitDamping);
        
        //transform.rotation = QT;

    }
}
