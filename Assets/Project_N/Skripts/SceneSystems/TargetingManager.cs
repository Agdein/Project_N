using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetingManager : MonoBehaviour
{
    
    public enum TargetingMode
    {
        GeneralMode,     //Общий режим
        PointSelection,  //Выбор точки для перемещения
        UnitTargeting,    //Выбор юнита для применения способности
        AoePointTargeting  //Выбор точки для способности аое, или другой, не таргетящей юнита
    }
   
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            //HandleMovementClick();
        }
        
        if (Input.GetMouseButtonDown(2)) // Средняя кнопка
        {
            //HandlePreviewClick();
        }
    }
    
    
    private bool IsPointerOverUI()   // Проверяем, находится ли курсор над UI элементом
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    
}
