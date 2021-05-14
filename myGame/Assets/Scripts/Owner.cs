using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Owner : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private AudioSource _walkSound;
    [SerializeField] private AudioClip _shotSound;

    [SerializeField] private float _speed;
    Transform _target;
    int m_CurrentWaypointIndex;

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    bool _stop = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _walkSound = GetComponentInChildren<AudioSource>();
    }

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        _walkSound.Play();
        StartCoroutine(Fire());
    }

    private void FixedUpdate()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
        _viewPoint.rotation = Quaternion.LookRotation(_target.position - _viewPoint.position);
        navMeshAgent.isStopped = _stop;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<IHealth>().HealthControl(0);
        }
    }
    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(0f);
        while (true)
        {
            RaycastHit hit;

            bool isHit = Physics.Raycast(_viewPoint.position, _viewPoint.forward, out hit, Mathf.Infinity);
            if (isHit && hit.collider.tag == "Player")
            {
                _animator.SetBool("Spotted", true);
                _stop = true;
                _walkSound.Stop();
                transform.rotation = Quaternion.LookRotation(_target.position - _viewPoint.position);
                _walkSound.PlayOneShot(_shotSound, 0.5f);
                var bullet = GameObject.Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Target = _target;
                yield return new WaitForSeconds(3f);
            }
            else
            {
                yield return new WaitForSeconds(3f);
                _stop = false;
                _walkSound.Play();
                _animator.SetBool("Spotted", false);
            }
        }
    }
}
