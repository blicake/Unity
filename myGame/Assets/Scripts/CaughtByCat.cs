using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtByCat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IHealth>().HealthControl(true);
        }
    }
}
