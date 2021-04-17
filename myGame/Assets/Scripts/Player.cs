using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHealth
{
    [SerializeField] private float _speed;
    [SerializeField] private float _sensitivity;

    private Vector3 _direction = Vector3.zero;
    private float _angle1;

    
    [SerializeField] private static int _health = 3;
    [SerializeField] private Canvas health;
    [SerializeField] private Image ImagePrefab;

    private Image[] hearts;
    Vector3 vector = new Vector3(50f, 0f, 0f);

    private void Start()
    {
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
        _direction.x = -Input.GetAxis("Horizontal");
        _direction.z = -Input.GetAxis("Vertical");

        _angle1 = Input.GetAxis("Mouse X");

        if (_health == 0) Death();
    }

    private void FixedUpdate()
    {
        var speed = _direction.normalized * _speed * Time.fixedDeltaTime;
        transform.Translate(speed);

        transform.Rotate(new Vector3(0f, _angle1 * _sensitivity * Time.fixedDeltaTime, 0f));
    }

    public void HealthControl(bool death)
    {
        if (death)
        {

            _health = 0;
        }

        else
        {
            Destroy(hearts[_health - 1]);
            _health--;
        }
    }

    private void Death()
    {
       for(int i = 0; i < hearts.Length; i++)
        {
            Destroy(hearts[i]);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  не работает
    }

}
