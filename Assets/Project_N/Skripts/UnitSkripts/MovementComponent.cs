using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementComponent : MonoBehaviour
{
    
    [Header("Характеристики")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveCostPerMeter = 1f;
    [SerializeField] private float rotationSpeed = 180f;
    
    [Header("Ссылки")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;

    
    
    void Start()
    {
        if (navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();
            
        if (animator == null)
            animator = GetComponent<Animator>();

        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.angularSpeed = rotationSpeed;
        }
    }

    void Update()
    {
        
    }
}
