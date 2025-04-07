using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private Sprite _frontFaceSprite;
   [SerializeField] private Sprite _midFrontFaceSprite;
   [SerializeField] private Sprite _middleFaceSprite;
   [SerializeField] private Sprite _midSideFaceSprite;
   [SerializeField] private Sprite _sideFaceSprite;
   
   [SerializeField] private Sprite _frontFaceSprite_TopPerspective;
   [SerializeField] private Sprite _midFrontFaceSprite_TopPerspective;
   [SerializeField] private Sprite _middleFaceSprite_TopPerspective;
   [SerializeField] private Sprite _midSideFaceSprite_TopPerspective;
   [SerializeField] private Sprite _sideFaceSprite_TopPerspective;

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
       float yRotation = _angleMeterY.transform.rotation.eulerAngles.y;
       float xRotation = _angleMeterX.transform.rotation.eulerAngles.x;

       _angleMeterX.transform.LookAt(_mainCamera.transform);
       //Debug.Log(xRotation);
       
      RotateTowardsCamera();
      

      if ((yRotation >= 345 && yRotation <= 360) || (yRotation >= 0 && yRotation <= 15) ||
          (yRotation >= 165 && yRotation <= 195))
      {
          if (CheckTopPerspective(xRotation))
          {
              ChangeSprite(_frontFaceSprite_TopPerspective, yRotation);
          }
          else
          {
              ChangeSprite(_frontFaceSprite, yRotation);
          }
      }
      else if((yRotation >= 75 && yRotation <= 105)|| (yRotation >= 255 && yRotation <= 285))
      {
          if (CheckTopPerspective(xRotation))
          {
              ChangeSprite(_sideFaceSprite_TopPerspective, yRotation);
          }
          else
          {
              ChangeSprite(_sideFaceSprite, yRotation);
          }
          
      }
      else if((yRotation >= 30 && yRotation <= 60) || (yRotation >= 120 && yRotation <= 150) ||
              (yRotation >= 210 && yRotation <= 240) || (yRotation >= 300 && yRotation <= 330))
      {
          if (CheckTopPerspective(xRotation))
          {
              ChangeSprite(_middleFaceSprite_TopPerspective, yRotation);
          }
          else
          {
              ChangeSprite(_middleFaceSprite, yRotation);
          }
      }
      else if ((yRotation > 15 && yRotation < 30) || (yRotation > 150 && yRotation < 165) ||
               (yRotation > 195 && yRotation < 210) || (yRotation > 330 && yRotation < 345))
      {
          if (CheckTopPerspective(xRotation))
          {
              ChangeSprite(_midFrontFaceSprite_TopPerspective, yRotation);
          }
          else
          {
              ChangeSprite(_midFrontFaceSprite, yRotation);
          }
      }
      else
      {
          if (CheckTopPerspective(xRotation))
          {
              ChangeSprite(_midSideFaceSprite_TopPerspective, yRotation);
          }
          else
          {
               ChangeSprite(_midSideFaceSprite, yRotation);
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

   private bool CheckTopPerspective(float angle)
   {
       if (angle > 325 && angle < 360)
       {
           return false;
       }
       else
       {
           return true;
       }
       
   }
   private void FadingMainSprite()
               {
                   var alfaColor = _spriteRenderer.color;
                   alfaColor.a -= ColorChangeSpeed;
                   _spriteRenderer.color = alfaColor;
               }
}
