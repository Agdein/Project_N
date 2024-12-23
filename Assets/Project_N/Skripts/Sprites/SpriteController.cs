using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
     [SerializeField] private Camera _camera; // Ссылка на камеру, назначается в инспекторе
    [SerializeField] private Sprite originalSprite; // Оригинальный спрайт
    [SerializeField] private Sprite originalSpriteReverce;
    [SerializeField] private Sprite alternateSprite; // Альтернативный спрайт
    [SerializeField] private Sprite alternateSpriteReverce;
    [SerializeField] float compressionFactor = 1f;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private bool ReverceSprite;
    private bool ReverceAlterSprite;
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

        float yRotation = transform.rotation.eulerAngles.y;
        
            RotateTowardsCamera();
    
            if ((yRotation >= 0 && yRotation <= 10) || (yRotation >= 70 && yRotation <= 90)|| (yRotation >= 170 && yRotation <= 190) || (yRotation >= 260 && yRotation <= 280) || (yRotation >= 350 && yRotation <= 360))
            {
                ReplaceSprite();
            }
            else if ((yRotation >= 45 && yRotation <= 90) || (yRotation >= 135 && yRotation <= 180) || (yRotation >= 225 && yRotation <= 270) || (yRotation >= 315 && yRotation <= 360))
            {
                ReverceSprite = true;
                ResetSprite();
            }
            else
            {
                ReverceSprite = false;
                ResetSprite();
            }
    }

    private void RotateTowardsCamera() //Поворот спрайта к камере
    {
        var camPosition = _camera.transform.position;
        camPosition.y = transform.position.y;
        transform.LookAt(camPosition);
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
