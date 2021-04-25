using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!GameObject.FindGameObjectWithTag("GameController"))
            {
                Destroy(GameObject.FindGameObjectWithTag("GameController"));
                Destroy(gameObject, 1f);
            }
        }
    }
}
