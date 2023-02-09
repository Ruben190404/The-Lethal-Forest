using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image OptionsPanel;
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
    
    public void Options()
    {
        Debug.Log("Click");
        OptionsPanel.gameObject.SetActive(true);
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }
}
