using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TurnManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    
    [SerializeField] private CameraOrbit _cameraOrbit;
   
    [Header("UI")]
    [SerializeField] private GameObject _unitPortrait;
    [SerializeField] private TextMeshProUGUI _unitName;
    
    
    [Header("Юниты")] 
    [SerializeField] public GameObject[] units;
    
    
    private Image _portraitImageComponent;
    
    public GameObject activeUnit;

    public event Action<GameObject> OnActiveUnitChanged;

    
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        uiManager.OnUnitButtonCLicked += ChangeActiveUnitEvent;
        
        
        _portraitImageComponent = _unitPortrait.GetComponent<Image>();
        
        DisableAllUnitsMovement();
        
        if (activeUnit == null)  ChangeActiveUnit(units[0]);
        
        //UpdateUI();
    }

    private void OnDestroy()
    {
        uiManager.OnUnitButtonCLicked -= ChangeActiveUnitEvent;
    }
    
    void Update()
    {
        
    }


    private void ChangeActiveUnitEvent(int index)
        {
            if (index >= 0 && index < units.Length)
            {
                ChangeActiveUnit(units[index]);
                OnActiveUnitChanged?.Invoke(units[index]);
            }
            
            
        }

    public void ChangeActiveUnit(GameObject unit)
    {
        if (activeUnit == null)
        {
            activeUnit = unit;
            activeUnit.GetComponent<MovementComponentOld>().enabled = true;
        }
        else
        {
            activeUnit.GetComponent<MovementComponentOld>().enabled = false;
            activeUnit.GetComponent<Unit>().activeUnit = false;
            
            activeUnit = unit;
            activeUnit.GetComponent<MovementComponentOld>().enabled = true;
            
            _cameraOrbit.ChangeFocalPoint(activeUnit.transform);
        }

        activeUnit.GetComponent<Unit>().activeUnit = true;
        //UpdateUI();


    }
    
    private void DisableAllUnitsMovement()
    {
        for (int i = 0; i < units.Length; i++)
        {
            units[i].GetComponent<MovementComponentOld>().enabled = false;
        }
    }
    
    /*private void UpdateUI()
    {
        if (activeUnit == null) return;
        
        // Обновляем имя
        Unit unitComponent = activeUnit.GetComponent<Unit>();
        if (unitComponent != null && _unitName != null)
        {
            _unitName.text = unitComponent.unitName;
        }
        
        // Обновляем портрет (ИМЕННО ЭТА СТРОКА МЕНЯЕТ UI!)
        if (unitComponent != null && _portraitImageComponent != null && unitComponent._portait != null)
        {
            _portraitImageComponent.sprite = unitComponent._portait;
        }
    }*/

    public void ResetActiveUnitStamina()
    {
      /*  if (activeUnit)
        {
            Unit unit = activeUnit.GetComponent<Unit>();
            if (unit)
            {
                unit.currentStamina = unit.maxStamina;
            }
        }*/
      
      
    } 
    
}
