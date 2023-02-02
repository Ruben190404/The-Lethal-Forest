using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GameInit : MonoBehaviour
{
    public GameObject Player;
    public GameObject Monster;
    public Transform SpawnPosition;

    // Start is called before the first frame update
    void Start()
    { 
        InitMonster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitMonster()
    {
        GameObject temp = Instantiate(Monster, SpawnPosition.position, Quaternion.identity);
        Player.GetComponent<PlayerDeath>().monsterAttack = Monster.GetComponent<MonsterAttack>();
        GetComponent<MonsterDifficulty>().monster = temp;
        temp.SetActive(false);
        Player.GetComponent<PlayerDeath>().Running = true;
    }

    void InitPlayer()
    {
    }
}
