using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    private AISensor _sensor;

    [Header("Initialization")] [SerializeField]
    private GameObject _player;
    public bool Teleported = false;
    
    [SerializeField] Collectible _collectible;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [Header("Wandering")] public float wanderRadius;
    [SerializeField] private Vector3 newPos;
    [SerializeField] private bool mayWait;
    [SerializeField] private float waitTimer;
    
    void Start()
    {
        _collectible = GameObject.Find("Player").GetComponent<Collectible>();
        Teleported = _collectible.Teleported;
        _sensor = GetComponent<AISensor>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        mayWait = true;
        getNewTarget();
    }
    
    void Update()
    {
        if (!_sensor.PlayerInSight)
        {
            _agent.SetDestination(newPos);
            if (!(_agent.remainingDistance > 0 + _agent.stoppingDistance) ||
                _agent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                if (mayWait)
                {
                    mayWait = false;
                    StartCoroutine(newTargetCooldown());
                }
            }

            _animator.SetInteger("State", 1);
        }

        if (Teleported)
        {
            getNewTarget();
            _collectible.Teleported = false;
        }
    }

    private void getNewTarget()
    {
        newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        mayWait = true;
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    IEnumerator newTargetCooldown()
    {
        yield return new WaitForSeconds(waitTimer);
        getNewTarget();
    }
}