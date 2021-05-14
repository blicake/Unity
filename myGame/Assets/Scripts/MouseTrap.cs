using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _closeSound;
    bool used = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _closeSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !used)
        {
            _closeSound.PlayOneShot(_closeSound.clip);
            _animator.SetTrigger("Trap");
            used = true;
            other.GetComponent<IHealth>().HealthControl(1);
        }
    }
}
