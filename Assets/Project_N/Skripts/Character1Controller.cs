using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character1Controller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackAnimationSpeed;

    private string currentAnimation;
    
    private Animator animator;
    
    private const string CHARACTER_IDLE = "Idle";
    private const string CHARACTER_BLOCK = "Block";
    private const string CHARACTER_MOVE_RIGHT = "Move_Right";
    private const string CHARACTER_MOVE_LEFT = "Move_Left";
    private const string CHARACTER_ATTACK = "Attack";
    
    
    Vector3 currentTransform;
 
    private void Start()
    {
        Vector3 currentTransform = GetComponent<Transform>().position;
        
        animator = GetComponent<Animator>();
    }
   
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float cameraMoveHorizontal = Input.GetAxis("Mouse X");
        float cameraMoveVertical = Input.GetAxis("Mouse Y");
        currentTransform = GetComponent<Transform>().position;

        if (moveHorizontal > 0) 
        {
            currentTransform.x = currentTransform.x + (speed/2);
            GetComponent<Transform>().position = currentTransform;
            Debug.Log(currentTransform);
            Debug.Log(moveHorizontal);
            ChangeAnimation(CHARACTER_MOVE_LEFT);
        }
        if (moveHorizontal < 0)
        {
            currentTransform.x = currentTransform.x - (speed/2);
            GetComponent<Transform>().position = currentTransform;
            Debug.Log(currentTransform);
            Debug.Log(moveHorizontal);
            ChangeAnimation(CHARACTER_MOVE_RIGHT);
        }
        if (moveVertical > 0)
        {
            currentTransform.z = currentTransform.z + speed;
            GetComponent<Transform>().position = currentTransform;
            Debug.Log(currentTransform);
            Debug.Log(moveHorizontal);
        }
        if (moveVertical < 0)
        {
            currentTransform.z = currentTransform.z - speed;
            GetComponent<Transform>().position = currentTransform;
            Debug.Log(currentTransform);
            Debug.Log(moveHorizontal);
            
        }

        if (moveHorizontal == 0 && moveVertical == 0 && currentAnimation != CHARACTER_ATTACK)
        {
            ChangeAnimation(CHARACTER_IDLE);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeAnimation(CHARACTER_ATTACK);
            Invoke("SetIdleAnimation", 5f);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            ChangeAnimation(CHARACTER_ATTACK);
                Debug.Log("NINEEEEEE");
                //Invoke("SetIdleAnimation", attackAnimationSpeed);
        }

    }

    void SetIdleAnimation()
    {
        ChangeAnimation(CHARACTER_IDLE);
    }
    void ChangeAnimation(string animationName)
                    {
                        if (animationName == currentAnimation && animationName != CHARACTER_ATTACK)
                        {
                            return;
                        }
                        animator.Play(animationName);
                        currentAnimation = animationName;
                    }
}
