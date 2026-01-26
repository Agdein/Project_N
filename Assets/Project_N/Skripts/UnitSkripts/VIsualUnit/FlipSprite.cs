using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform _localRotation;
    [SerializeField] private SpriteRenderer _sprite;
    private float _normalScale;
    [SerializeField, Range(0.5f, 1f)] private float _compressedScaleDegree = 0.8f;
    private float _yRotation;
    private bool _isCompressed = false;
    
    void Start()
    {
        if(!_localRotation) _localRotation = this.transform;
        if (!_sprite) _sprite = this.GetComponent<SpriteRenderer>();
        _normalScale = this.transform.localScale.x;
        _compressedScaleDegree = _normalScale * _compressedScaleDegree;

    }

    void Update()
    {
        _yRotation = _localRotation.localRotation.eulerAngles.y;

        if (_yRotation > 0 && _yRotation < 180) _sprite.flipX = true;
        if (_yRotation < 360 && _yRotation > 180) _sprite.flipX = false;
        if (_yRotation > 0 && _yRotation < 20 || _yRotation > 340 && _yRotation < 360 ||
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
          
        
        

    }
    
    private static void ChangeScale(Transform transform, float x)
    {
        Vector3 scale = transform.localScale;
        scale.x = x;
        transform.localScale = scale;
    }
    
}
