using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Transform Player;
    private float AttackRange = 3f;
    public float Attack;
    [SerializeField]private float TimePassed;
    private Animator anim;
    [SerializeField]private bool Attacking;
    public bool PlayerKilled = false;
    
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }
    
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
    
    private void Hit()
    {
        if (Vector3.Distance(transform.position, Player.position) <= AttackRange && !PlayerKilled)
        {
            PlayerKilled = true;
        }
        Attacking = false;
        Attack = 0;
        TimePassed = 0f;
    }
}
