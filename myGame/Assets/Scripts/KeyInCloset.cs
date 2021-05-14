using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInCloset : MonoBehaviour
{
    [SerializeField] private Button Key;

    [SerializeField] private Button Cloth1;
    [SerializeField] private Button Cloth2;
    [SerializeField] private Button Cloth3;
    [SerializeField] private Button Cloth4;
    [SerializeField] private Button Cloth5;
    [SerializeField] private Button Cloth6;
    [SerializeField] private Button Cloth7;

    [SerializeField] private Canvas health;
    [SerializeField] private Image ImagePrefab;
    [SerializeField] private AudioSource _pickSound;

    private void Awake()
    {
        _pickSound = GetComponent<AudioSource>();

        Key.onClick.AddListener(GetKey);

        Cloth1.onClick.AddListener(Remove1);
        Cloth2.onClick.AddListener(Remove2);
        Cloth3.onClick.AddListener(Remove3);
        Cloth4.onClick.AddListener(Remove4);
        Cloth5.onClick.AddListener(Remove5);
        Cloth6.onClick.AddListener(Remove6);
        Cloth7.onClick.AddListener(Remove7);
    }

    private void Remove1()
    {
        Cloth1.gameObject.SetActive(false);
    }

    private void Remove2()
    {
        Cloth2.gameObject.SetActive(false);
    }

    private void Remove3()
    {
        Cloth3.gameObject.SetActive(false);
    }

    private void Remove4()
    {
        Cloth4.gameObject.SetActive(false);
    }

    private void Remove5()
    {
        Cloth5.gameObject.SetActive(false);
    }

    private void Remove6()
    {
        Cloth6.gameObject.SetActive(false);
    }

    private void Remove7()
    {
        Cloth7.gameObject.SetActive(false);
    }

    private void GetKey()
    {
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        _pickSound.PlayOneShot(_pickSound.clip);
        yield return new WaitForSeconds(_pickSound.clip.length);
        gameObject.SetActive(false);
        Key.gameObject.SetActive(false);
        Instantiate(ImagePrefab, health.transform);
        gameObject.SetActive(false);
        
    }
}
