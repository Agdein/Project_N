using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
     [SerializeField] private Camera camera; // Ссылка на камеру, назначается в инспекторе
    [SerializeField] private Sprite originalSprite; // Оригинальный спрайт
    [SerializeField] private Sprite alternateSprite; // Альтернативный спрайт
    [SerializeField] float compressionFactor = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Action<object> _log;

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
        if (angle >= 0 && angle < 40)
        {
            // Поворачиваем спрайт к камере
            RotateTowardsCamera();
            ResetSprite();
            _log(angle);
        }
        else if (angle >= 40 && angle < 70)
        {
            // Сжимаем спрайт по оси Y и поворачиваем его к камере
            ResetSprite();
            CompressAndRotateTowardsCamera();
            _log(angle);
        }
        else if (angle >= 70 && angle <= 90)
        {
            // Заменяем спрайт на другой и поворачиваем к камере
            ReplaceSpriteAndRotateTowardsCamera();
            _log(angle);
        }
        else if (angle >= 90 && angle < 130)
        {
            // Сжимаем спрайт по оси Y и поворачиваем его к камере
            ResetSprite();
            CompressAndRotateTowardsCamera();
            _log(angle);
        }
        else if (angle >= 130 && angle < 180)
        {
            // Сжимаем спрайт по оси Y и поворачиваем его к камере
            ResetSprite();
            RotateTowardsCamera();
            _log(angle);
        }
    }

    void RotateTowardsCamera()
    {
        transform.LookAt(camera.transform.position);
    }

    void CompressAndRotateTowardsCamera()
    {
        transform.LookAt(camera.transform.position);
        transform.localScale = new Vector3(originalScale.x * compressionFactor, originalScale.y, originalScale.z);
    }

    void ReplaceSpriteAndRotateTowardsCamera()
    {
        transform.LookAt(camera.transform.position);
        if (spriteRenderer.sprite != alternateSprite)
        {
            spriteRenderer.sprite = alternateSprite;
        }
    }

    void ResetSprite()
    {
        if (spriteRenderer.sprite != originalSprite)
        {
            spriteRenderer.sprite = originalSprite;
        }
        transform.localScale = originalScale;
    }
}
