using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{ 
    // === ИДЕНТИФИКАЦИЯ ===
    public string unitId;            // Уникальный ID для сохранений
    public string unitName;          // Имя для UI
    public Faction faction;          // Player/Enemy/Neutral
    public Sprite _portait;
    
    
    
    // === ХАРАКТЕРИСТИКИ ===
    public bool activeUnit = false; 
    
    public int maxHealth = 100;
    public int currentHealth = 100;
    
    public float maxStamina = 1000;      // Для перемещения
    public float currentStamina = 1000;
    
    public int maxActionPoints = 2;  // Действия за ход
    public int currentActionPoints = 2;
    
    public int initiative = 10;      // Для порядка хода
    public int moveRange = 5;        // Дальность перемещения
    public float moveCostPerMeter = 1f; // Стоимость перемещения
    
    // === СОБЫТИЯ которые публикует Unit ===
    /*public event System.Action<Unit, int, int> OnHealthChanged;     // old, new
    */
    public event System.Action<Unit, float, float> OnStaminaChanged;  /*  // old, new
    
    public event System.Action<Unit, int, int> OnActionPointsChanged;
    
    public event System.Action<Unit> OnUnitSelected;
    public event System.Action<Unit> OnUnitDeselected;
    public event System.Action<Unit> OnUnitDied;
    public event System.Action<Unit> OnUnitMoved;
    public event System.Action<Unit, Ability> OnAbilityUsed;
    
    public event System.Action<Unit> OnTurnStarted;
    public event System.Action<Unit> OnTurnEnded;


    [SerializeField] public MovementComponentOld _movementComponent;*/

    [SerializeField] private Animator _animator;


    private string _currentAnim;
    private string _IdleAnim = "Idle";
    private string _walkAnim = "Walk";

   


    void Start()
    {
        _currentAnim = _IdleAnim;
    }

    void Update()
    {
        
    }
    
    public bool SpendStamina(float amount)
    {
        if (currentStamina < amount) return false;

        float oldStamina = currentStamina;
        currentStamina -= amount;
        
        OnStaminaChanged?.Invoke(this, oldStamina, currentStamina);
        return true;
    }
    
    public void ResetActiveUnitStamina()
    {
        float oldStamina = currentStamina;
        currentStamina = maxStamina;
        OnStaminaChanged?.Invoke(this, oldStamina, currentStamina);

    }

    public void ChangeUnitAnimtion()
    {

        if (_currentAnim == _IdleAnim)
        {
             _animator.Play(_walkAnim);
             _currentAnim = _walkAnim;
        }
        if (_currentAnim == _walkAnim)
        {
            _animator.Play(_IdleAnim);
            _currentAnim = _IdleAnim;
        }
        
    }

    
    
    
    
}

public enum Faction
{
    Player,
    Enemy,
    Neutral    
}

public enum Class
{
    Warrior,    
    Archer,    
    Mage,    
    Healer,    
    Scout,     
    Tank      
}

public enum UnitAnimation
{
    Idle,      
    Walk,
    Ability
}

