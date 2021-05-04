using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatsStart : MonoBehaviour
{
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform wpStart1;
    [SerializeField] private Transform wpFinish1;
    [SerializeField] private Transform wpStart2;
    [SerializeField] private Transform wpFinish2;

    [SerializeField] private float speed1;
    [SerializeField] private float speed2;

    GameObject _cat1;
    GameObject _cat2;

    void Awake()
    {
        _cat1 = GameObject.Instantiate(catPrefab, wpStart1.position, Quaternion.identity);
        _cat2 = GameObject.Instantiate(catPrefab, wpStart2.position, Quaternion.identity);

        _cat1.GetComponent<Cat>().waypoints[0] = wpStart1;
        _cat1.GetComponent<Cat>().waypoints[1] = wpFinish1;
        _cat2.GetComponent<Cat>().waypoints[0] = wpStart2;
        _cat2.GetComponent<Cat>().waypoints[1] = wpFinish2;

        _cat1.GetComponent<NavMeshAgent>().speed = speed1;
        _cat2.GetComponent<NavMeshAgent>().speed = speed2;
    }
}
