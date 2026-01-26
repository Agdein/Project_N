using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("Systems")] 
    [SerializeField] public TargetingManager _targetingManager;
    [SerializeField] public TurnManager _turnManager;
    [SerializeField] public CameraOrbit _cameraOrbit;
    
    [Header("UI")]
    [SerializeField] public Image unitPortraitUI;
    [SerializeField] public TextMeshProUGUI unitNameUI; 
    [SerializeField] public TextMeshProUGUI _currentStaminaText;
    [SerializeField] public Slider _staminaSlider;
    [SerializeField] public TextMeshProUGUI _pathCostText;
    
    [Header("Кнопки выбора юнита")]
    [SerializeField] private Button[] unitButtons;
    
    [SerializeField] public Button _resetStaminaButton;
    
    [SerializeField] public Button _changeAnimButton;

    private GameObject _activeUnit;
    
    private GameObject _newUnitEventLink;
    private GameObject _oldUnitEventLink;
    
    
    
    public event Action<int> OnUnitButtonCLicked;
    
    
    void Start()
    {
        for (int i = 0; i < unitButtons.Length; i++)
        {
            int index = i;
            unitButtons[i].onClick.AddListener(() => UnitButtonCLicked(index));
        }
        _turnManager.OnActiveUnitChanged += UpdateUI;
        _turnManager.OnActiveUnitChanged += ActiveUnitChanged;

        _resetStaminaButton.onClick.AddListener(() => ResetUnitStamina());
        
        _changeAnimButton.onClick.AddListener(() => ChangeAnimFufu() );

        UnitButtonCLicked(0);

    }

    private void OnDestroy()
    {
        _turnManager.OnActiveUnitChanged -= UpdateUI;
        _turnManager.OnActiveUnitChanged -= ActiveUnitChanged;
    }

    void Update()
    {
        //_currentStaminaText.text = _activeUnit.GetComponent<Unit>().currentStamina.ToString();
         //UpdateUI();
    }

    private void UnitButtonCLicked(int index)
    {
        OnUnitButtonCLicked?.Invoke(index);
    }
    
    public void UpdateUI(GameObject NewUnit)
    {
        if (NewUnit == null) return;
        
        
        // Обновляем имя
        Unit unitComponent = NewUnit.GetComponent<Unit>();
        if (unitComponent != null && unitNameUI != null)
        {
            unitNameUI.text = unitComponent.unitName;
        }
        
        // Обновляем портрет (ИМЕННО ЭТА СТРОКА МЕНЯЕТ UI!)
        if (unitComponent != null && unitPortraitUI != null && unitComponent._portait != null)
        {
            unitPortraitUI.sprite = unitComponent._portait;
        }
        
        //Обновляю ссылку на текст стамины
        _currentStaminaText.text = $"{Convert.ToInt32(unitComponent.currentStamina)}";
        _staminaSlider.value = unitComponent.currentStamina;

    }


    private void ActiveUnitChanged(GameObject newUnit)
    {
        if(_newUnitEventLink) 
            _oldUnitEventLink = _newUnitEventLink;
        _newUnitEventLink = newUnit;
        newUnit.GetComponent<Unit>().OnStaminaChanged += CurrentStaminaChanged;
        if(_oldUnitEventLink) 
            _oldUnitEventLink.GetComponent<Unit>().OnStaminaChanged -= CurrentStaminaChanged;
    }

    private void CurrentStaminaChanged(Unit unit, float old, float nev)
    {
        UpdateUI(unit.gameObject);
    }

    public void ResetUnitStamina()
    {
        _turnManager.activeUnit.GetComponent<Unit>().ResetActiveUnitStamina();
    }

    private void ChangeAnimFufu()
    {

        if (_turnManager.activeUnit == _turnManager.units[1])
        {
            _turnManager.activeUnit.GetComponent<Unit>().ChangeUnitAnimtion();
        }
    }



}
