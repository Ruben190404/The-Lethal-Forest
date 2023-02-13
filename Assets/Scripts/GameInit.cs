using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameInit : MonoBehaviour
{
    public GameObject Player;
    public GameObject Monster;
    public Transform SpawnPosition;
    private Collectible _collectible;
    private bool BGMplaying = false;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip _audioClip2;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _player;


    void Start()
    {
        InitMonster();
        _audioSource.clip = _audioClip;
        _audioSource.Play();
        _collectible = Player.GetComponent<Collectible>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_collectible.ItemsCollected == 1 && !BGMplaying)
        {
            _audioSource.clip = _audioClip2;
            _audioSource.Play();
            _audioSource.volume = 0.5f;
            BGMplaying = true;
        }

        if (PlayerPrefs.HasKey("FOV"))
        {
            _camera.fieldOfView = PlayerPrefs.GetFloat("FOV") * 100;
        }
        else
        {
            _camera.fieldOfView = 60;
        }

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            _player.GetComponent<PlayerMovement>().CameraSensitivity = PlayerPrefs.GetFloat("Sensitivity") * 100;
        }
        else
        {
            _player.GetComponent<PlayerMovement>().CameraSensitivity = 30f;
        }

        if (PlayerPrefs.HasKey("PPS"))
        {
            if (PlayerPrefs.GetInt("PPS") == 0)
            {
                _camera.GetComponent<PostProcessLayer>().enabled = false;
            }
            else
            {
                _camera.GetComponent<PostProcessLayer>().enabled = true;
            }
        }
        else
        {
            _camera.GetComponent<PostProcessLayer>().enabled = true;
        }
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    void InitMonster()
    {
        GameObject temp = Instantiate(Monster, SpawnPosition.position, Quaternion.identity);
        Player.GetComponent<PlayerDeath>().monsterAttack = Monster.GetComponent<MonsterAttack>();
        GetComponent<MonsterDifficulty>().monster = temp;
        temp.SetActive(false);
        Player.GetComponent<PlayerDeath>().Running = true;
    }
}