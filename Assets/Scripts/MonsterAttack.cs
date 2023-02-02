using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Transform Player;
    private float AttackRange = 1f;
    public float Attack;
    [SerializeField]private float TimePassed;
    private Animator anim;
    [SerializeField]private bool Attacking;
    public bool PlayerKilled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TimePassed += Time.deltaTime;
        InRange();
    }

    void InRange()
    {
        if (
            Vector3.Distance(transform.position, Player.position) <= AttackRange && 
            !Attacking && 
            TimePassed >= 3f && 
            !PlayerKilled
            )
        {
            Attacking = true;
            Attack = Random.Range(1 ,3);
        }
    }
    
    void Hit()
    {
        if (Vector3.Distance(transform.position, Player.position) <= AttackRange)
        {
            PlayerKilled = true;
        }
        Attacking = false;
        Attack = 0;
        TimePassed = 0f;
    }
}
