using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
     [SerializeField] private Camera camera; // Ссылка на камеру, назначается в инспекторе
    [SerializeField] private Sprite originalSprite; // Оригинальный спрайт
    [SerializeField] private Sprite originalSpriteReverce;
    [SerializeField] private Sprite alternateSprite; // Альтернативный спрайт
    [SerializeField] private Sprite alternateSpriteReverce;
    [SerializeField] float compressionFactor = 1f;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Action<object> _log;
    private bool ReverceSprite;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        if (originalSprite != null)
        {
            spriteRenderer.sprite = originalSprite; // Устанавливаем оригинальный спрайт
        }
    }

    void Update()
    {
        Vector3 directionToCamera = camera.transform.position - transform.position;
        float angle = Vector3.Angle(Vector3.forward, directionToCamera);
        _log = Debug.Log;
        

        if (transform.localRotation.y < 0 && transform.localRotation.y > -180)
        {
            ReverceSprite = true;
        }
        else
        {
            ReverceSprite = false;
        }
        
        if (angle >= 0 && angle < 40)
        {
            // Поворачиваем спрайт к камере
            RotateTowardsCamera();
            ResetSprite();
            //_log(angle);
        }
        else if (angle >= 40 && angle < 70)
        {
            
            ResetSprite();
            CompressSprite();
            RotateTowardsCamera();
            
        }
        else if (angle >= 70 && angle <= 90)
        {
            
            ReplaceSprite();
            RotateTowardsCamera();
           
        }
        else if (angle >= 90 && angle < 130)
        {
            // Сжимаем спрайт по оси Y и поворачиваем его к камере
            ResetSprite();
            CompressSprite();
            RotateTowardsCamera();
         
        }
        else if (angle >= 130 && angle < 180)
        {
            // Сжимаем спрайт по оси Y и поворачиваем его к камере
            ResetSprite();
            RotateTowardsCamera();
         
        }

      
        
        
    }

    private void RotateTowardsCamera()
    {
        var camPosition = camera.transform.position;
        camPosition.y = transform.position.y;
        transform.LookAt(camPosition);
        //_log(ReverceSprite);
        //_log(transform.localRotation.eulerAngles.y);
        
    }

    private void CompressSprite()// Сжимаем спрайт по оси Y и поворачиваем его к камере
    {
        transform.localScale = new Vector3(originalScale.x * compressionFactor, originalScale.y, originalScale.z);
    }

    private void ReplaceSprite()// Заменяем спрайт на другой и поворачиваем к камере
    {
        if(ReverceSprite!)
        { if (spriteRenderer.sprite != alternateSprite)
            {
                spriteRenderer.sprite = alternateSprite;
            }
           
        }
        else
        {if (spriteRenderer.sprite != alternateSpriteReverce)
            {
                spriteRenderer.sprite = alternateSpriteReverce;
            }
           
        }
    }

    private void ResetSprite()
    {
       if(ReverceSprite!)
       { if (spriteRenderer.sprite != originalSprite)
        {
            spriteRenderer.sprite = originalSprite;
        }
           
       }
       else
       {if (spriteRenderer.sprite != originalSpriteReverce)
           {
               spriteRenderer.sprite = originalSpriteReverce;
           }
          
       }
        transform.localScale = originalScale;
    }
}
