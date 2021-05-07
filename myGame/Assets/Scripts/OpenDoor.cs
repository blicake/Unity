using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Key"))
            {
                _animator.SetTrigger("Open");
                Destroy(GameObject.FindGameObjectWithTag("Key"));
            }
        }
    }
}
