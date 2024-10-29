using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private float attackAnimationSpeed;

    private string currentAnimation;
    
    private Animator animator;
    
    private const string CHARACTER_IDLE = "Idle";
    private const string CHARACTER_BLOCK = "Block";
    private const string CHARACTER_MOVE_RIGHT = "Move_Right";
    private const string CHARACTER_MOVE_LEFT = "Move_Left";
    private const string CHARACTER_ATTACK = "Attack";
    private const string CHARACTER_JUMP = "Jump";
    
    private void Start()
    {
        Vector3 currentTransform = GetComponent<Transform>().position;
        
        animator = GetComponent<Animator>();
    }
   
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
       

        if (moveHorizontal > 0) 
        {
            ChangeAnimation(CHARACTER_MOVE_LEFT);
        }
        if (moveHorizontal < 0)
        {
            ChangeAnimation(CHARACTER_MOVE_RIGHT);
        }
        if (moveVertical > 0)
        {
           
        }
        if (moveVertical < 0)
        {

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
        if(Input.GetKey(KeyCode.F))
        {
            ChangeAnimation(CHARACTER_ATTACK);
                Debug.Log("NINEEEEEE");
                //Invoke("SetIdleAnimation", attackAnimationSpeed);
        }

        if (Input.GetButtonDown("Jump"))
        {
            ChangeAnimation(CHARACTER_JUMP);
        }

    }

    void SetIdleAnimation()
    {
        ChangeAnimation(CHARACTER_IDLE);
    }
    void ChangeAnimation(string animationName)
                    {
                        if (animationName == currentAnimation && animationName != CHARACTER_ATTACK && animationName != CHARACTER_JUMP)
                        {
                            return;
                        }
                        animator.Play(animationName);
                        currentAnimation = animationName;
                    }
}

