using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;

    bool triggerOn = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IHealth>().HealthControl(0);
        }
        if (other.tag == "Sausage")
        {
            Destroy(other);
            triggerOn = true;
            StartCoroutine(FoundSausage(other.gameObject));
        }
    }


    protected void FixedUpdate()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !triggerOn)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
    private IEnumerator FoundSausage(GameObject _sausage)
    {
        navMeshAgent.SetDestination(_sausage.transform.position);
        navMeshAgent.isStopped = true;
        _animator.SetBool("FoundSausage", true);
        yield return new WaitForSeconds(7f);
        navMeshAgent.isStopped = false;
        Destroy(_sausage);
        _animator.SetBool("FoundSausage", false);
        triggerOn = false;
    }
}
