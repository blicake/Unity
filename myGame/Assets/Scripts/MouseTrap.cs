using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    bool used = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !used)
        {
            _animator.SetTrigger("Trap");
            used = true;
            other.GetComponent<IHealth>().HealthControl(1);
        }
    }
}
