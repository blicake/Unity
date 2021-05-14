using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    private Vector3 _targetPosition;
    [SerializeField] private float speed;

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }

    public Transform Target
    {
        set
        {
            _target = value;
            _targetPosition = _target.position;
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IHealth>().HealthControl(1);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
