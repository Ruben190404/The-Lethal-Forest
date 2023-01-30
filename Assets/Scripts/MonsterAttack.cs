using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Transform Player;
    private float AttackRange = 1f;
    public float Attack;
    private float TimePassed;
    private Animator anim;
    private bool Attacking;
    
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
        //check if player is in range
        if (Vector3.Distance(transform.position, Player.position) <= AttackRange && !Attacking)
        {
            Attacking = true;
            Hit();
        }
        
    }
    
    void Hit()
    {
        Attack = Random.Range(1 ,3);
        Debug.Log("Hit");
    }
    
    void AttackStop()
    {
        Attack = 0;
        Attacking = false;
    }
}
