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
    private float TimePassed;
    private float Attack;
    MonsterDifficulty monsterDifficulty;
    [SerializeField] MonsterAttack monsterAttack;
    [SerializeField] float MonsterSpeed;

    private enum AnimStates
    {
        Idle,
        Walking,
        Rage,
        Attack,
        Attack2,
        Dead
    }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        monsterAttack = GetComponent<MonsterAttack>();
        monsterDifficulty = GameObject.Find("MonsterDifficulty").GetComponent<MonsterDifficulty>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack = monsterAttack.Attack;
        MonsterSpeed = monsterDifficulty.MonsterSpeed;
        agent.speed = MonsterSpeed;
        UpdateAnimationState();
        TimePassed += Time.deltaTime;
        if (TimePassed >= 1f)
        {
            SetDestination();
            TimePassed = 0f;
        }
    }

    void UpdateAnimationState()
    {
        if (agent.velocity.magnitude > 0f && Attack == 0)
        {
            anim.SetInteger("State", (int)AnimStates.Walking);
        }
        else if (Attack == 1)
        {
            anim.SetInteger("State", (int)AnimStates.Attack);
        }
        else if (Attack == 2)
        {
            anim.SetInteger("State", (int)AnimStates.Attack2);
        }
        else
        {
            anim.SetInteger("State", (int)AnimStates.Idle);
        }
    }
    
    void SetDestination()
    {
        agent.SetDestination(player.position);
    }
}