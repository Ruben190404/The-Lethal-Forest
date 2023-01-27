using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterMovement : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    private Rigidbody rb;
    private Animator anim;
    private float CurrentSpeed;

    private enum AnimStates
    {
        Idle,
        Walking,
        Rage,
        Attack,
        Dead
    }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        if (agent.velocity.magnitude > 0f)
        {
            anim.SetInteger("State", (int)AnimStates.Walking);
        }
    
        else
        {
            anim.SetInteger("State", (int)AnimStates.Idle);
        }
        
    }
}