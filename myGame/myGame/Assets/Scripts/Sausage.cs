using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sausage : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image ImagePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(ImagePrefab, canvas.transform);
        }
    }
}
