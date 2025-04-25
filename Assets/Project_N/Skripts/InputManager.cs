using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static event Action EMoveForvard;
    public static event Action EMoveBack;
    public static event Action EMoveRight;
    public static event Action EMoveLeft;
    public static event Action EJump;             
    
    
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            EMoveForvard.Invoke();  
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            EMoveBack.Invoke();
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            EMoveRight.Invoke();
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            EMoveLeft.Invoke();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            EJump.Invoke();
        }
    }
}
