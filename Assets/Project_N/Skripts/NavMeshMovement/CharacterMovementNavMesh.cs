using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementNavMesh : MonoBehaviour
{
// MoveDestination.cs
    public Transform goal;
    public NavMeshAgent agent;
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                agent.destination = hit.point;
            }
            
        }
        
        /*agent.destination = goal.position;
        Debug.Log(agent.destination);*/
       
    }
} 