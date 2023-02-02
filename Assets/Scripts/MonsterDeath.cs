using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterDeath : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private NavMeshAgent agent;
    [SerializeField]private bool Dead;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if (Dead && anim.GetInteger("State") != 5)
        {
            agent.velocity = Vector3.zero;
            // agent.isStopped = true;
            anim.SetInteger("State", 2);
        }
    }
    
    void Die()
    {
        agent.velocity = Vector3.zero;
        
        anim.SetInteger("State", 5);
    }
    
    void Victory()
    {
        Debug.Log("Victory!!!");
    }
}
