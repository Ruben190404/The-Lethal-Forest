using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    private bool _pps;
    private float _fovValue;
    private float _sensitivityValue;
    private bool _fullscreen;
    
    [SerializeField] private GameObject _ppsToggle;
    [SerializeField] private GameObject _fullscreenToggle;
    [SerializeField] private GameObject _sensitivitySlider;
    [SerializeField] private GameObject _fovSlider;
    [SerializeField] private TextMeshProUGUI _sensitivityText;
    [SerializeField] private TextMeshProUGUI _fovText;
    [SerializeField] private Image _optionsPanel;


    private void Update()
    {
        PPSvalue();
        FullscreenValue();
        SensitivityValue();
        FOVvalue();
    }

    void PPSvalue()
    {
        if (PlayerPrefs.HasKey("PPS"))
        {
            _pps = Convert.ToBoolean(PlayerPrefs.GetInt("PPS"));
            _ppsToggle.GetComponent<Toggle>().isOn = _pps;
        }
        else
        {
            PlayerPrefs.SetInt("PPS", 1);
        }
    }
    
    void FullscreenValue()
    {
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            _fullscreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
            _fullscreenToggle.GetComponent<Toggle>().isOn = _fullscreen;
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
    }
    
    void SensitivityValue()
    {
        _sensitivityValue = _sensitivitySlider.GetComponent<Slider>().value * 100;
        _sensitivityText.text = _sensitivityValue.ToString("F0");
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            _sensitivitySlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
        {
            PlayerPrefs.SetFloat("Sensitivity", (30f / 100f));
        }
    }
    
    void FOVvalue()
    {
        _fovValue = _fovSlider.GetComponent<Slider>().value * 100;
        _fovText.text = _fovValue.ToString("F0");
        if (PlayerPrefs.HasKey("FOV"))
        {
            _fovSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("FOV");
        }
        else
        {
            PlayerPrefs.SetFloat("FOV", (60f / 100f));
        }
    }

    public void SensitivitySlide()
    {
        PlayerPrefs.SetFloat("Sensitivity", _sensitivitySlider.GetComponent<Slider>().value);
    }
    
    public void FOVSlide()
    {
        PlayerPrefs.SetFloat("FOV", _fovSlider.GetComponent<Slider>().value);
    }
    
    public void PPSToggle()
    {
        if (_pps)
        {
            PlayerPrefs.SetInt("PPS", 0);
        }
        else
        {
            PlayerPrefs.SetInt("PPS", 1);
        }
    }
    
    public void FullscreenToggle()
    {
        if (_fullscreen)
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
    }

    public void Return()
    {
        _optionsPanel.gameObject.SetActive(false);
    }
}
