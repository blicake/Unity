using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject.transform.parent.gameObject);
            other.GetComponent<IHealth>().HealthControl(1);
        }
    }
}
