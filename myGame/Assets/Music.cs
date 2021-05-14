using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        music.Play();
    }
}
