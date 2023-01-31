using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    MonsterAttack monsterAttack;
    bool Dead;
    
    [SerializeField] private TextMeshProUGUI DeathText;
    [SerializeField] private Image DeathPanel;

    // private void Start()
    // {
    //     monsterAttack = GameObject.FindWithTag("Monster").GetComponent<MonsterAttack>();
    //     Dead = monsterAttack.PlayerKilled;
    // }

    void Update()
    {
        if (monsterAttack == null)
        {
            monsterAttack = GameObject.FindWithTag("Monster").GetComponent<MonsterAttack>();
        }
        
        Dead = monsterAttack.PlayerKilled;
        
        if (Dead)
        {
            StartCoroutine(Fade(true));
            // go to main menu after 2 seconds
        }
    }

    IEnumerator Fade(bool Fading)
    {
        if (Fading)
        {
            for (float i = 0; i <= 1; i += (Time.deltaTime / 2f))
            {
                DeathPanel.color = new Color(0, 0, 0, 1);
                DeathText.color = new Color(0.68f, 0, 0, i);
                yield return null;
            }
        }
    }
}
