using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerDeath : MonoBehaviour
{
    public MonsterAttack monsterAttack;
    public bool Dead;
    private bool DeathScreen;
    private Rigidbody rb;
    public bool Running;
    
    [SerializeField] private TextMeshProUGUI DeathText;
    [SerializeField] private Image DeathPanel;
    [SerializeField] private float WaitTimer;
    [SerializeField] private int Deaths;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Running && GameObject.FindWithTag("Monster") != null)
        {
                monsterAttack = GameObject.FindWithTag("Monster").GetComponent<MonsterAttack>();
                Dead = monsterAttack.PlayerKilled;
                if (Dead)
                {
                    rb.velocity = Vector3.zero;
                    StartCoroutine(Fade(true));
                    WaitTimer += Time.deltaTime;
                }

                LoadScene();
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
        else
        {
            for (float i = 1; i >= 0; i -= (Time.deltaTime / 2f))
            {
                DeathPanel.color = new Color(0, 0, 0, 0);
                DeathText.color = new Color(0.68f, 0, 0, i);
                yield return null;
            }
        }
    }
    
    void LoadScene()
    {
        if (WaitTimer >= 2 && !DeathScreen)
        {
            DeathScreen = true;
            Deaths++;
            Debug.Log(Deaths);
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            Debug.Log(PlayerPrefs.GetInt("Deaths"));
            // SceneManager.LoadScene(0);
            WaitTimer = 0;
        }
    }
}
