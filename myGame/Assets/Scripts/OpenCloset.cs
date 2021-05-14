using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloset : MonoBehaviour
{
    [SerializeField] private GameObject MiniGame;
    [SerializeField] private GameObject Text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                Text.SetActive(false);
                MiniGame.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Text.SetActive(false);
        if (GameObject.FindGameObjectWithTag("Key"))
        {
            Destroy(gameObject.GetComponent<OpenCloset>());
        }
    }
}
