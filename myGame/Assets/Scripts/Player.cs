using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHealth, ISausage
{
    [SerializeField] private float _speed;
    public float _sensitivity;

    private Vector3 _direction = Vector3.zero;
    private float _angle1;

    
    [SerializeField] private int _health = 3;
    [SerializeField] private Canvas health;
    [SerializeField] private Image ImagePrefab;

    [SerializeField] private GameObject Sausage;
    [SerializeField] private Transform SausagePrefab;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject OptionsMenu;

    private Image[] hearts;
    Vector3 vector = new Vector3(100f, 0f, 0f);

    bool _sausage = false;
    Image SausageImage;

    private void Awake()
    {
        PauseMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }

    private void Start()
    {
        SetSensitivity(PauseMenu.GetComponent<Menu>().Sensitivity);
        hearts = new Image[_health]; 

        for (int i = 0; i < _health; i++)
        {
            Vector3 _vector = vector;
            _vector.x *= i;

            hearts[i] = Instantiate<Image>(ImagePrefab, ImagePrefab.transform.position + _vector, Quaternion.identity, health.transform);
            
        }
    }

    private void Update()
    {
        if (!PauseMenu.activeInHierarchy)
        {
            _direction.x = -Input.GetAxis("Horizontal");
            _direction.z = -Input.GetAxis("Vertical");

            _angle1 = Input.GetAxis("Mouse X");

            if (_health == 0) Death();
        }

        if(!PauseMenu.activeInHierarchy && !health.gameObject.activeInHierarchy)
        {
            health.gameObject.SetActive(true);
        }
        

        if (Input.GetMouseButtonDown(0) && !PauseMenu.activeInHierarchy)
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
            if (!PauseMenu.activeInHierarchy)
            {
                PauseMenu.SetActive(true);
                health.gameObject.SetActive(false);
            }
            else
            {
                if (!OptionsMenu.activeInHierarchy)
                {
                    PauseMenu.SetActive(false);
                    health.gameObject.SetActive(true);
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
                Destroy(hearts[_health - 1]);
                _health --;
                break;
            case 2:
                if(_health < 3)
                {
                    Vector3 _vector = vector;
                    _vector.x *= _health;
                    hearts[_health] = Instantiate<Image>(ImagePrefab, ImagePrefab.transform.position + _vector, Quaternion.identity, health.transform);
                }
                break;
        }
        
    }

    private void Death()
    {
       for(int i = 0; i < hearts.Length; i++)
        {
            Destroy(hearts[i]);
            _health = 0;
        }
        SceneManager.LoadScene(1);
    }

    void ISausage.Sausage(bool sausage, Image _object)
    {
        if (sausage) 
        {
            _sausage = true;
            SausageImage = _object;
        }
    }

    private void SetSensitivity(float value)
    {
        _sensitivity = value;
    }
}
