using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private Image Panel;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI EndText;
    [SerializeField] private TextMeshProUGUI WinsAmount;
    [SerializeField] private GameObject CreditList;
    public bool FadeEnded;

    void Start()
    {
        foreach (var Text in CreditList.GetComponentsInChildren<TextMeshProUGUI>())
        {
            Text.color = Color.black;
        }
        CreditList.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        StartCoroutine(Fade());
        if (PlayerPrefs.GetInt("Wins") > 1)
        {
            WinsAmount.text = "Wins: " + PlayerPrefs.GetInt("Wins");
        }
        else
        {
            WinsAmount.text = "Congrats on your first win!";
        }
    }

    private void Update()
    {
        if (Panel.transform.position.y > -30000 && FadeEnded)
        {
            foreach (var Text in CreditList.GetComponentsInChildren<TextMeshProUGUI>())
            {
                Text.color = Color.white;
            }
            Panel.transform.position += new Vector3(0, 0.1f, 0);
        }
        
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           SceneManager.LoadScene("Main Menu");
       }
    }

    IEnumerator Fade()
    {
        for (float i = 0; i <= 1; i += (Time.deltaTime / 2f))
        {
            Title.color = new Color(0.68f, 0, 0, i);
            yield return null;
            if (i >= 0.9f)
            {
                StopCoroutine(Fade());
                FadeEnded = true;
            }
        }
    }
}
