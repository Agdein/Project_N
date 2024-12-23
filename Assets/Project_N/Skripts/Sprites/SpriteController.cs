using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
     [SerializeField] private Camera _camera; // Ссылка на камеру, назначается в инспекторе
    [SerializeField] private Sprite directSprite; // Оригинальный спрайт
    [SerializeField] private Sprite turningRightSprite;
    [SerializeField] private Sprite turningLeftSprite; // Альтернативный спрайт
    [SerializeField] private Sprite turningDirectlySprite;
    [SerializeField] float compressionFactor = 1f;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private bool ReverceAlterSprite;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        originalScale = transform.localScale;

        if (directSprite != null)
        {
            spriteRenderer.sprite = directSprite; // Устанавливаем оригинальный спрайт
        }
    }

    void Update()
    {

        float yRotation = transform.rotation.eulerAngles.y;
        
            RotateTowardsCamera();
    
            if ((yRotation >= 0 && yRotation <= 10) || (yRotation >= 70 && yRotation <= 90)|| (yRotation >= 170 && yRotation <= 190) || (yRotation >= 260 && yRotation <= 280) || (yRotation >= 350 && yRotation <= 360))
            {
                SetDirectSprite();
                ResetSprite();
            }
            else if ((yRotation >= 40 && yRotation <= 50) || (yRotation >= 130 && yRotation <= 140) || (yRotation >= 220 && yRotation <= 230) || (yRotation >= 310 && yRotation <= 320))
            {
               SetTurningDirectSprite();
               ResetSprite();
            }
            else if ((yRotation >= 45 && yRotation <= 90) || (yRotation >= 135 && yRotation <= 180) || (yRotation >= 225 && yRotation <= 270) || (yRotation >= 315 && yRotation <= 360))
            {
                
                SetTurningRightSprite();
                ResetSprite();
            }
            else
            {
                SetTurningRightSprite();
                ReverceSprite();
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

    private void SetDirectSprite()
    {
        if (spriteRenderer.sprite != directSprite)
        {
                spriteRenderer.sprite = directSprite;
        }
    }

    private void SetTurningRightSprite()
    {
       
       if (spriteRenderer.sprite != turningRightSprite)
        {
            spriteRenderer.sprite = turningRightSprite;
        }

    }

    private void SetTurningDirectSprite()
    {
        if (spriteRenderer.sprite != turningDirectlySprite)
        {
            spriteRenderer.sprite = turningDirectlySprite;
        }

    }

    private void ResetSprite()
    {
         transform.localScale = originalScale;
    }

    private void ReverceSprite()
    {
        var CurrentPosition = transform.localScale;
        if (CurrentPosition.x > 0)
        {
             CurrentPosition.x = CurrentPosition.x * (-1);
                    transform.localScale = CurrentPosition;
        }

       
    }
}
