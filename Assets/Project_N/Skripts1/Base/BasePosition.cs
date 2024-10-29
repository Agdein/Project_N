using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePosition : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private float positionS;
 

    // Update is called once per frame
    void Update()
    {
        Vector3 position = _player.position;
        position.y -= positionS;
        transform.position = position;
    }
}
