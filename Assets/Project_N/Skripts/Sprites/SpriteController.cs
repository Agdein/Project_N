using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = System.Drawing.Color;

public class SpriteController : MonoBehaviour
{
   /* [SerializeField] private Camera _camera; // Ссылка на камеру, назначается в инспекторе
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

        if (_camera == null)
        {
            Camera cam = Camera.main;
            _camera = cam;
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


    }  */

   [SerializeField] private Sprite _frontFaceSprite;
   [SerializeField] private Sprite _midFrontFaceSprite;
   [SerializeField] private Sprite _middleFaceSprite;
   [SerializeField] private Sprite _midSideFaceSprite;
   [SerializeField] private Sprite _sideFaceSprite;

   [SerializeField] private GameObject _angleMeterX;
   [SerializeField] private GameObject _angleMeterY;
   [SerializeField] private SpriteRenderer _subSpriteRenderer;

   [SerializeField] private float ColorChangeSpeed = 0.2f;

   private Camera _mainCamera;
   private SpriteRenderer _spriteRenderer;
   private Animator _animator;
   private UnityEngine.Color _baseColor;

   void Start()
   {
       if (_spriteRenderer == null)
       {
           _spriteRenderer = GetComponent<SpriteRenderer>();
       }

       if (_mainCamera == null)
       {
           _mainCamera = Camera.main;
       }

       _baseColor = _spriteRenderer.color;
   }

   void Update()
   {
      /* var a = _spriteRenderer.color;
       a.a -= 1 ;
       _spriteRenderer.color = UnityEngine.Color.Lerp(_spriteRenderer.color, a, Mathf.Abs(Mathf.Sin(Time.time)));
*/

      /* Debug.Log(_spriteRenderer.color.a);
      if (!ololo)
      {
          var a = _spriteRenderer.color;
          a.a -= ColorChangeSpeed;
          _spriteRenderer.color = a;

          if (_spriteRenderer.color.a <= 0)
          {
              ololo = true;
          }


      }
      if (ololo)
      {
          var a = _spriteRenderer.color;
          a.a += ColorChangeSpeed;
          _spriteRenderer.color = a;

          if (_spriteRenderer.color.a >= 1)
          {
              ololo = false;
          }
      }
      */

      float yRotation = _angleMeterY.transform.rotation.eulerAngles.y;


     RotateTowardsCamera();

      if ((yRotation >= 345 && yRotation <= 360) || (yRotation >= 0 && yRotation <= 15) ||
         (yRotation >= 165 && yRotation <= 195))
      {
         ChangeSprite(_frontFaceSprite, yRotation);
      }
      else if((yRotation >= 75 && yRotation <= 105)|| (yRotation >= 255 && yRotation <= 285))
      {
          if (_sideFaceSprite != null)
          {
              ChangeSprite(_sideFaceSprite, yRotation);
          }
          else
          {
              ChangeSprite(_frontFaceSprite, yRotation);
          }
      }
      else if((yRotation >= 30 && yRotation <= 60) || (yRotation >= 120 && yRotation <= 150) ||
              (yRotation >= 210 && yRotation <= 240) || (yRotation >= 300 && yRotation <= 330))
      {
         ChangeSprite(_middleFaceSprite, yRotation);
      }
      else if ((yRotation > 15 && yRotation < 30) || (yRotation > 150 && yRotation < 165) ||
               (yRotation > 195 && yRotation < 210) || (yRotation > 330 && yRotation < 345))
      {
          ChangeSprite(_midFrontFaceSprite, yRotation);
      }
      else
      {
          if (_midSideFaceSprite != null)
          {
              ChangeSprite(_midSideFaceSprite, yRotation);
          }
          else
          {
              ChangeSprite(_midFrontFaceSprite, yRotation);
          }

      }
   }

   private void RotateTowardsCamera() //Поворот спрайта к камере
   {
       var camPosition = _mainCamera.transform.position;
       camPosition.y = transform.position.y;
      _angleMeterY.transform.LookAt(camPosition);
   }

   private void ChangeSprite(Sprite sprite, float angle)
   {
       bool flip = CheckFlipSprite(angle);

       if (_spriteRenderer.sprite != sprite)
       {

           _subSpriteRenderer.enabled = true;
           _subSpriteRenderer.sprite = sprite;
           if (flip)
           {
               _subSpriteRenderer.flipX = true;
           }
           else
           {
               _subSpriteRenderer.flipX = false;
           }

           if (_spriteRenderer.color.a <= 0)
           {
               if (flip)
               {
                   _spriteRenderer.flipX = true;
               }
               else
               {
                   _spriteRenderer.flipX = false;
               }
               _spriteRenderer.color = _baseColor;
               _spriteRenderer.sprite = sprite;
           }
           else
           {
               FadingMainSprite();
           }

       }
       else
       {
        //Debug.Log(sprite.name);
       }

   }
   private bool CheckFlipSprite(float angle)
   {

       if ((angle > 105 && angle < 195) || (angle > 255 && angle < 345))
       {
           return true;
       }
       else
       {
           return false;
       }
   }
   private void FadingMainSprite()
               {
                   var alfaColor = _spriteRenderer.color;
                   alfaColor.a -= ColorChangeSpeed;
                   _spriteRenderer.color = alfaColor;
               }
}
