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
            var _object = Instantiate(ImagePrefab, canvas.transform);
            other.GetComponent<ISausage>().Sausage(true, _object);
        }
    }
}
