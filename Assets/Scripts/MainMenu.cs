using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DeathText;
    private int DeathAmount = 0;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DeathAmount = PlayerPrefs.GetInt("Deaths");
        
        if (DeathAmount == 0)
        {
            DeathText.text = "";
        }
        else
        {
            DeathText.text = "Deaths: " + DeathAmount;
        }
        Cursor.visible = true;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    
    public void CustomButton()
    {
        Debug.Log("Custom Button Pressed");
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }
}
