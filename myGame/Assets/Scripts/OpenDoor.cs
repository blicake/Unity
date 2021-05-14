using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _openSound;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _openSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Key"))
            {
                _openSound.PlayOneShot(_openSound.clip);
                _animator.SetTrigger("Open");
                Destroy(GameObject.FindGameObjectWithTag("Key"));
            }
        }
    }
}
