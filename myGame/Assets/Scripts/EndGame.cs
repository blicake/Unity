using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject ImagePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Instantiate(ImagePrefab, canvas.transform);
            Time.timeScale = 0;
            StartCoroutine(Ending());
            
        }
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(0f);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                yield return new WaitForSeconds(0f);
                SceneManager.LoadScene(0);
            }
            yield return new WaitForSeconds(0f);
        }
        
    }
}
