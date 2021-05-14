using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    [SerializeField] private AudioSource _pickSound;
    private void Awake()
    {
        _pickSound = GetComponent<AudioSource>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(PlaySound(other));
        }
    }

    private IEnumerator PlaySound(Collider other)
    {
        _pickSound.PlayOneShot(_pickSound.clip);
        yield return new WaitForSeconds(_pickSound.clip.length);
        gameObject.SetActive(false);
        Destroy(gameObject);
        other.GetComponent<IHealth>().HealthControl(2);
    }
}
