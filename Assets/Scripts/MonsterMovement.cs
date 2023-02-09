using UnityEngine;
using UnityEngine.AI;


public class MonsterMovement : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    private Rigidbody rb;
    private Animator anim;
    private float CurrentSpeed;
    private float Attack;
    MonsterDifficulty monsterDifficulty;

    [SerializeField] MonsterAttack monsterAttack;
    [SerializeField] float MonsterSpeed;
    [SerializeField] float AnimationSpeed;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioClip _audioClip2;

    private enum AnimStates
    {
        Idle,
        Walking,
        Attack,
        Attack2,
    }

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        monsterAttack = GetComponent<MonsterAttack>();
        monsterDifficulty = GameObject.Find("MapInit").GetComponent<MonsterDifficulty>();
    }
    
    void Update()
    {
        Attack = monsterAttack.Attack;
        MonsterSpeed = monsterDifficulty.MonsterSpeed;
        agent.speed = MonsterSpeed;
        UpdateAnimationState();

        AnimationSpeed = MonsterSpeed;
        if (monsterDifficulty.Scream)
        {
            Scream();
            monsterDifficulty.Scream = false;
        }
    }

    void UpdateAnimationState()
    {
        if (agent.velocity.magnitude > 0f && Attack == 0)
        {
            anim.SetInteger("State", (int)AnimStates.Walking);
            anim.speed = AnimationSpeed;
        }
        else if (Attack == 1)
        {
            anim.SetInteger("State", (int)AnimStates.Attack);
            anim.speed = 1f;
        }
        else if (Attack == 2)
        {
            anim.SetInteger("State", (int)AnimStates.Attack2);
            anim.speed = 1f;
        }
        else if (anim.GetInteger("State") != 5)
        {
            anim.SetInteger("State", (int)AnimStates.Idle);
            anim.speed = 1f;
        }
    }

    void SetDestination()
    {
        agent.SetDestination(player.position);
    }

    void Step()
    {
        if (!monsterDifficulty.Scream)
        {
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }

    void Scream()
    {
        _audioSource.clip = _audioClip2;
        _audioSource.Play();
    }
}