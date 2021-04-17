using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cats : MonoBehaviour
{
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform wpStart1;
    [SerializeField] private Transform wpFinish1;
    [SerializeField] private Transform wpStart2;
    [SerializeField] private Transform wpFinish2;

    [SerializeField] private float speed1 = 3f;
    [SerializeField] private float speed2 = 3f;
    [SerializeField] private float turnSpeed1 = 3f;
    [SerializeField] private float turnSpeed2 = 3f;

    GameObject _cat1;
    GameObject _cat2;

    bool start1;
    bool start2;
    bool finish1;
    bool finish2;

    void Awake()
    {
        _cat1 = GameObject.Instantiate(catPrefab, wpStart1.position, Quaternion.identity);
        _cat2 = GameObject.Instantiate(catPrefab, wpStart2.position, Quaternion.identity);

        start1 = true;
        start2 = true;

    }

    void Update()
    {
        if (start1) 
        { 
            _cat1.transform.position = Vector3.MoveTowards(_cat1.transform.position, wpFinish1.position, speed1 * Time.deltaTime);
            _cat1.transform.rotation = Quaternion.RotateTowards(_cat1.transform.rotation, wpFinish1.rotation, turnSpeed1 * Time.deltaTime);
        }
        if (start2)
        {
            _cat2.transform.position = Vector3.MoveTowards(_cat2.transform.position, wpFinish2.position, speed2 * Time.deltaTime);
            _cat2.transform.rotation = Quaternion.RotateTowards(_cat2.transform.rotation, wpFinish2.rotation, turnSpeed2 * Time.deltaTime);
        }

        if (_cat1.transform.position == wpFinish1.position) start1 = false;
        if (_cat2.transform.position == wpFinish2.position) start2 = false;

        if (!start1)
        {
            _cat1.transform.position = Vector3.MoveTowards(_cat1.transform.position, wpStart1.position, speed1 * Time.deltaTime);
            _cat1.transform.rotation = Quaternion.RotateTowards(_cat1.transform.rotation, wpStart1.rotation, turnSpeed1 * Time.deltaTime);
        }
        if (!start2)
        {
            _cat2.transform.position = Vector3.MoveTowards(_cat2.transform.position, wpStart2.position, speed2 * Time.deltaTime);
            _cat2.transform.rotation = Quaternion.RotateTowards(_cat2.transform.rotation, wpStart2.rotation, turnSpeed2 * Time.deltaTime);
        }

        if (_cat1.transform.position == wpStart1.position) start1 = true;
        if (_cat2.transform.position == wpStart2.position) start2 = true;
    }

}
