using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class MovementComponentOld : MonoBehaviour
{
    
    private NavMeshAgent agent;
    public float staminaPerMeter = 2f;
     
    public ref float currentStamina => ref this.GetComponent<Unit>().currentStamina;
    
   //public float currentStamina => GetComponent<Unit>().currentStamina;

    
    //Ссылки на системы
    [SerializeField] private GameObject SceneSystems;
    [SerializeField] private TargetingManager _targetingManager;
    [SerializeField] private UIManager _uiManager;
    
    
    private MovementSystem _movementSystem;
    private AbilitySystem _abilitySystem;
    private Unit _unitComponent;
    
    
    private void Awake()
    {
      
    }

    void Start()
    {
        SceneSystems= GameObject.FindGameObjectWithTag("System");

        _targetingManager = SceneSystems.GetComponent<TargetingManager>();
        _uiManager = SceneSystems.GetComponent<UIManager>();
        _unitComponent = this.GetComponent<Unit>();
        
      
      //  _textMeshPro = _uiManager.GetComponent<UIManager>()._currentStaminaText;
      //  _textMeshPro2 = _uiManager.GetComponent<UIManager>()._pathCostText;
       // _textMeshPro3 = _uiManager.GetComponent<UIManager>()._currentStaminaText;
       // _staminaSlider = _uiManager.GetComponent<UIManager>()._staminaSlider;
      //  _resetStaminaButton = _uiManager.GetComponent<UIManager>()._resetStaminaButton;
    
        agent = GetComponent<NavMeshAgent>();

        //currentStamina = this.GetComponent<Unit>().currentStamina;
        
//        _resetStaminaButton.onClick.AddListener(ResetStamina);
    }

    void Update()
    {
       // _textMeshPro2.text = $"Current stamina: {Convert.ToInt32(currentStamina)}";
       // _textMeshPro.text = $"{Convert.ToInt32(currentStamina)}";
       // _staminaSlider.value = currentStamina;
        // ПРЕДПРОСМОТР СТОИМОСТИ при наведении мыши
        if (Input.GetMouseButtonDown(2))
        {
             PreviewPathCost();
        }
       
        
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            MoveToMousePosition();
        }
    }
    
    private bool IsPointerOverUI()
    {
        // Проверяем, находится ли курсор над UI элементом
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void StaminasssChanged(Vector3 vector3)
    {
        Debug.Log( $"Stamina{vector3}");
    }
    
    void PreviewPathCost()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            {
                float cost = CalculatePathCost(navHit.position);
                
                // Меняем цвет курсора в зависимости от доступности
                if (cost <= currentStamina)
                {
                    // Зеленый - можно идти
                    ShowCostPreview(navHit.position, cost, Color.green);
                }
                else
                {
                    // Красный - нельзя
                    ShowCostPreview(navHit.position, cost, Color.red);
                }
            }
        }
    }
    
    void MoveToMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            {
                float cost = CalculatePathCost(navHit.position);
                
                /*if (cost <= currentStamina)
                {
                    agent.SetDestination(navHit.position);
                    currentStamina -= cost;
                    
                    // Событие для UI
                    StaminaChanged?.Invoke(currentStamina);
                }*/

                if (_unitComponent.SpendStamina(cost))
                {
                    agent.SetDestination(navHit.position);
                }

                


            }
        }
    }
    
    float CalculatePathCost(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(targetPosition, path))
        {
            float pathLength = GetPathLength(path);
            return pathLength * staminaPerMeter;
        }
        return Mathf.Infinity;
    }
    
    float GetPathLength(NavMeshPath path)
    {
        float length = 0f;
        for (int i = 1; i < path.corners.Length; i++)
        {
            length += Vector3.Distance(path.corners[i-1], path.corners[i]);
        }
        return length;
    }
    
    void ShowCostPreview(Vector3 position, float cost, Color color)
    {
        // Здесь можно добавить UI для показа стоимости
       // _textMeshPro3.text = $"Path cost: {cost:F1}";
       _uiManager._pathCostText.text = $"Path cost: {cost:F1}";
        Debug.Log($"Стоимость пути: {cost:F1} очков");
    }

    public void ResetStamina()
    {
        if (GetComponent<Unit>().activeUnit)
        {
            this.GetComponent<Unit>().currentStamina = 1000;
        }
    }
    
    // Событие для обновления UI
    public System.Action<float> StaminaChanged;
}