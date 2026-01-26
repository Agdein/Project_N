using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSprite : MonoBehaviour
{
    [Header("Настройки поворота")]
    [Tooltip("Целевая камера. Если не назначена, будет использоваться Camera.main")]
    public Transform targetCamera;
    
    [Tooltip("Скорость поворота (0 = мгновенный поворот)")]
    public float rotationSpeed = 5f;
    
    [Tooltip("Игнорировать вертикальную составляющую (рекомендуется true)")]
    public bool ignoreY = true;
    
    [Header("Опции")]
    [Tooltip("Автоматически искать главную камеру при старте")]
    public bool findMainCameraOnStart = true;
    
    [Tooltip("Обновлять поворот каждый кадр")]
    public bool updateEveryFrame = true;
    
    void Start()
    {
        // Находим камеру, если она не назначена вручную
        if (targetCamera == null && findMainCameraOnStart)
        {
            FindMainCamera();
        }
    }
    
    void Update()
    {
        if (updateEveryFrame && targetCamera != null)
        {
            LookAtTarget();
        }
    }
    
    void LateUpdate()
    {
        // Альтернатива: использовать LateUpdate если нужно поворачиваться после всех обновлений
        // if (updateEveryFrame && targetCamera != null)
        // {
        //     LookAtTarget();
        // }
    }
    
    /// <summary>
    /// Основной метод поворота к камере
    /// </summary>
    public void LookAtTarget()
    {
        if (targetCamera == null) return;
        
        // Вычисляем направление к камере
        Vector3 direction = targetCamera.position - transform.position;
        
        // Игнорируем вертикальную составляющую (делаем поворот только по горизонтали)
        if (ignoreY)
        {
            direction.y = 0;
        }
        
        // Если направление не нулевое (объект не находится прямо над/под камерой)
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            // Мгновенный или плавный поворот
            if (rotationSpeed <= 0)
            {
                transform.rotation = targetRotation;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    targetRotation, 
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }
    
    /// <summary>
    /// Найти главную камеру в сцене
    /// </summary>
    public void FindMainCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            targetCamera = mainCamera.transform;
           // Debug.Log($"Найдена главная камера: {targetCamera.name}", this);
        }
        else
        {
         //   Debug.LogWarning("Главная камера не найдена в сцене!", this);
        }
    }
    
    /// <summary>
    /// Установить новую цель для поворота
    /// </summary>
    /// <param name="newTarget">Новая цель (камера или другой объект)</param>
    public void SetTarget(Transform newTarget)
    {
        targetCamera = newTarget;
    }
    
    /// <summary>
    /// Включить/выключить постоянное отслеживание
    /// </summary>
    /// <param name="enabled">Включить отслеживание</param>
    public void SetUpdateEnabled(bool enabled)
    {
        updateEveryFrame = enabled;
    }
    
    /// <summary>
    /// Одноразовый поворот к текущей цели
    /// </summary>
    public void LookAtTargetOnce()
    {
        if (targetCamera != null)
        {
            // Временно отключаем плавность для мгновенного поворота
            float originalSpeed = rotationSpeed;
            rotationSpeed = 0;
            LookAtTarget();
            rotationSpeed = originalSpeed;
        }
    }
}
