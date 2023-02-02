using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterDeath : MonoBehaviour
{
    
    [SerializeField] Collectible collectible;
    private Animator anim;
    private Rigidbody rb;
    private NavMeshAgent agent;
    [SerializeField]private bool Dead;
    float ItemsCollected;
    int TotalItems;

    void Start()
    {
        collectible = GameObject.Find("Player").GetComponent<Collectible>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        TotalItems = collectible.TotalItems;
    }

    
    void Update()
    {
        ItemsCollected = collectible.ItemsCollected;
        Dead = ItemsCollected == TotalItems; 
        
        if (Dead && anim.GetInteger("State") != 5)
        {
            int currentState = anim.GetInteger("State");
            if (currentState != 5)
            {
                agent.velocity = Vector3.zero;
                // agent.isStopped = true;
                anim.SetInteger("State", 2);
                anim.speed = 1f;
            }
            else
            {
                Victory();
            }
        }
    }
    
    void Die()
    {
        anim.SetInteger("State", 5);
    }
    
    void Victory()
    {
        SceneManager.LoadScene(2);
    }
}
