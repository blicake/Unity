using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Owner : MonoBehaviour
{
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _speed;
    Transform _target;
    int m_CurrentWaypointIndex;

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    bool _stop = false;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
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
                _stop = true;
                transform.rotation = Quaternion.LookRotation(_target.position - _viewPoint.position);
                var bullet = GameObject.Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Target = _target;
                Debug.DrawRay(_viewPoint.position, _viewPoint.forward, Color.green);
                yield return new WaitForSeconds(3f);
            }
            else
            {
                Debug.DrawRay(_viewPoint.position, -_viewPoint.forward, Color.red);
                yield return new WaitForSeconds(3f);
                _stop = false;
            }
        }
    }
}
