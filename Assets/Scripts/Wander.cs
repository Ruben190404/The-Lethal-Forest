using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    [Header("Initialization")]
        [SerializeField] private GameObject _player;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [Header("Wandering")]
        public float wanderRadius;
        [SerializeField] private Vector3 newPos;
        [SerializeField] private bool mayWait;
        [SerializeField] private float waitTimer;
        // Start is called before the first frame update
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            mayWait = true;
            getNewTarget();
        }

        // Update is called once per frame
        void Update()
        {
            _agent.SetDestination(newPos);
            Debug.Log(_agent.destination);
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

        private void getNewTarget()
        {
            newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            mayWait = true;
        }
        private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
            Vector3 randDirection = Random.insideUnitSphere * dist;
 
            randDirection += origin;
 
            NavMeshHit navHit;
 
            NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
            return navHit.position;
        }

        IEnumerator newTargetCooldown()
        {
            yield return new WaitForSeconds(waitTimer);
            getNewTarget();
        }
    }