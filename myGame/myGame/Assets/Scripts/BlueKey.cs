using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueKey : MonoBehaviour
{
    [SerializeField] private Canvas health;
    [SerializeField] private Image ImagePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(ImagePrefab, health.transform);
        }
    }
}
