using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHealth, ISausage
{
    [SerializeField] private AudioSource _walkSound;
    [SerializeField] private float _speed;
    public float _sensitivity;

    private Vector3 _direction = Vector3.zero;
    private float _angle1;

    
    [SerializeField] private int _health = 3;
    [SerializeField] private Canvas health;
    [SerializeField] private Image ImagePrefab;

    [SerializeField] private GameObject Sausage;
    [SerializeField] private Transform SausagePrefab;

    private Image[] hearts;

    bool _sausage = false;
    Image SausageImage;

    private void Awake()
    {
        _sensitivity = 100 * Menu.instance.MouseSensitivity;
        _walkSound = gameObject.GetComponentInChildren<AudioSource>();
        _walkSound.Play();
        _walkSound.Pause();
    }

    private void Start()
    {
        hearts = new Image[_health]; 

        hearts = health.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        bool playsound;
        

        if (Menu.instance.gameObject.activeInHierarchy && Menu.instance.OptionsMenu.activeInHierarchy)
        {
            _sensitivity = 100 * Menu.instance.MouseSensitivity;
        }
        if (!Menu.instance.gameObject.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
            {
                playsound = true;
            }
            else playsound = false;

            if (!playsound)
            {
                _walkSound.Pause();
            }
            else
            {
                _walkSound.UnPause();
            }

            _direction.x = -Input.GetAxis("Horizontal");
            _direction.z = -Input.GetAxis("Vertical");

            _angle1 = Input.GetAxis("Mouse X");

            if (_health == 0) Death();
        }

        if(!Menu.instance.gameObject.activeInHierarchy && !health.gameObject.activeInHierarchy)
        {
            health.gameObject.SetActive(true);
        }
        

        if (Input.GetMouseButtonDown(0) && !Menu.instance.gameObject.activeInHierarchy)
        {
            if (_sausage)
            {
                Instantiate(Sausage, SausagePrefab.position + transform.position, Quaternion.identity);
                _sausage = false;
                Destroy(SausageImage);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Menu.instance.gameObject.activeInHierarchy)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                Menu.instance.gameObject.SetActive(true);
            }
            else
            {
                if (!Menu.instance.OptionsMenu.activeInHierarchy)
                {
                    Menu.instance.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    AudioListener.pause = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        var speed = _direction.normalized * _speed * Time.fixedDeltaTime;
        transform.Translate(speed);

        transform.Rotate(new Vector3(0f, _angle1 * _sensitivity * Time.fixedDeltaTime, 0f));

    }

    public void HealthControl(int healthChange)
    {
        switch (healthChange)
        {
            case 0:
                Death();
                break;
            case 1:
                hearts[_health - 1].gameObject.SetActive(false);
                _health --;
                break;
            case 2:
                if(_health < 3)
                {
                    hearts[_health].gameObject.SetActive(true);
                    _health++;
                }
                break;
        }
        
    }

    private void Death()
    {
       for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(false);
            _health = 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ISausage.Sausage(bool sausage, Image _object)
    {
        if (sausage) 
        {
            _sausage = true;
            SausageImage = _object;
        }
    }
}
