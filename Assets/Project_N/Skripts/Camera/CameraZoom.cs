using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraZoom : MonoBehaviour
{
    private Vector3 _position;
    private float _minPosition;
    [SerializeField] private float _zoomScale = 10;
    void Start()
    {
        _minPosition = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        _position = transform.localPosition;

        // Получаем значение прокрутки колесика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Если прокрутка вверх (положительное значение)
        if (scroll > 0 & (transform.localPosition.z < _minPosition))
        {
            _position.z += _zoomScale;
                transform.localPosition = _position;
        }
        // Если прокрутка вниз (отрицательное значение)
        else if (scroll < 0)
        {
            _position.z -= _zoomScale;
            transform.localPosition = _position;
        }
    }
}
