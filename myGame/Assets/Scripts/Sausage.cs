using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sausage : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image ImagePrefab;
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
        var _object = Instantiate(ImagePrefab, canvas.transform);
        other.GetComponent<ISausage>().Sausage(true, _object);
    }
}
