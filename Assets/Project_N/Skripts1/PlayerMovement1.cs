using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _jump = 5;
    [SerializeField] private Transform _cam;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {

    float horInput = Input.GetAxisRaw("Horizontal") * _moveSpeed;
    float verInput = Input.GetAxisRaw("Vertical") * _moveSpeed;
    
    //camera direction

    Vector3 camForward = _cam.forward;
    Vector3 camRight = _cam.right;

    camForward.y = 0;
    camRight.y = 0;
    
    //creating relate camera direction
    Vector3 forvardRelative = verInput * camForward;
    Vector3 rightRelative = horInput * camRight;

    Vector3 moveDir = forvardRelative + rightRelative;
    
    //movement
    
    //rb.velocity = new Vector3(horInput, rb.velocity.y, verInput);
    rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
    
    if(Input.GetButton("Jump") && rb.velocity.y < 1000)
        // Mathf.Approximately - это приблизительное сравнение, в данном случае будет выводить тру при нуля или коло того.
       //Mathf.Approximately(0, rb.velocity.y)
    {
        rb.velocity = new Vector3(horInput, _jump, verInput);
    }
      //transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);  //(поворачивает объект в сторону его движения)
      
    }
    
}
