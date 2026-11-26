using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSkeleton : MonoBehaviour
{
    [SerializeField] private Transform _localRotation;
    //[SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _spriteFlipper;
    private float _normalScale;
    [SerializeField, Range(0.5f, 1f)] private float _compressedScaleDegree = 0.8f;
    private float _yRotation;
    private bool _isCompressed = false;
    
    void Start()
    {
        if(!_localRotation) _localRotation = this.transform;
        //if (!_sprite) _sprite = this.GetComponent<SpriteRenderer>();
        _normalScale = this.transform.localScale.x;
        _compressedScaleDegree = _normalScale * _compressedScaleDegree;

    }

    void Update()
    {
        _yRotation = _localRotation.localRotation.eulerAngles.y;

        if (_yRotation > 0 && _yRotation < 180) FlipObject(_spriteFlipper, true);
        if (_yRotation < 360 && _yRotation > 180) FlipObject(_spriteFlipper, false);
        /*if (_yRotation > 0 && _yRotation < 20 || _yRotation > 340 && _yRotation < 360 ||
            _yRotation > 160 && _yRotation < 200)
        {
            if (!_isCompressed) 
            {
                ChangeScale(_localRotation, _compressedScaleDegree);
                _isCompressed = true;
            }
        }
        else
        {
            if (_isCompressed) 
            {
                ChangeScale(_localRotation, _normalScale);
                _isCompressed = false;
            }
        }
          */
        
        

    }
    
    private static void ChangeScale(Transform transform, float x)
    {
        Vector3 scale = transform.localScale;
        scale.x = x;
        transform.localScale = scale;
    }

    private static void FlipObject(Transform transform, bool flip)
    {
        Vector3 scale = transform.localScale;
        if (flip == true)
        { 
            if (scale.x > 0)
            {
                 scale.x = scale.x * -1;
            }
        }
        else
        { 
            if (scale.x < 0)
            {
                scale.x = scale.x * -1;
            }
        }
        transform.localScale = scale;
    }
}
