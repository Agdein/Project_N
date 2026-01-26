using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ClickableObject : MonoBehaviour
    {
        void OnMouseDown() // Работает ТОЛЬКО если есть Collider
        {
            Debug.Log("Клик по " + gameObject.name);
            // Твой код здесь
        }
    }

