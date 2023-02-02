using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI WinsAmount;
    
    void Start()
    {
        StartCoroutine(Fade());
        if (PlayerPrefs.GetInt("Wins") > 1)
        {
            WinsAmount.text = "Wins: " + PlayerPrefs.GetInt("Wins");
        }
        else
        {
            WinsAmount.text = "First Win!";
        }
    }

    IEnumerator Fade()
    {
        for (float i = 0; i <= 1; i += (Time.deltaTime / 2f))
        {
            Title.color = new Color(0.68f, 0, 0, i);
            yield return null;
            Debug.Log(i);
            if (i >= 0.9f)
            {
                StopCoroutine(Fade());
                StartCoroutine(CreditsScroll());
            }
        }
    }
    
    IEnumerator CreditsScroll()
    {
        while (panel.transform.position.y < 1000)
        {
            panel.transform.position += new Vector3(0, 0.01f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
