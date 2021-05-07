using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _options;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _closeMenu;

    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private Slider _volume;
    [SerializeField] private Button _closeOptionsMenu;

    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _menu;

    public float Sensitivity;

    int scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        if (scene == 0) _start.onClick.AddListener(StartGame);
        if(scene != 0) _continue.onClick.AddListener(ContinueGame);
        _options.onClick.AddListener(OptionsMenu);
        _exit.onClick.AddListener(EndGame);

        if (scene != 0) _closeMenu.onClick.AddListener(CloseMenu);
        _closeOptionsMenu.onClick.AddListener(CloseOptionsMenu);

        _sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        SetSensitivity(_sensitivitySlider.value);
    }

    private void SetSensitivity(float value)
    {
        Sensitivity = value * 100;
    }

    private void CloseOptionsMenu()
    {
        _optionsMenu.SetActive(false);
    }

    private void CloseMenu()
    {
        _menu.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _optionsMenu.activeInHierarchy)
        {
            _optionsMenu.SetActive(false);
        }
    }

    private void OptionsMenu()
    {
        _optionsMenu.SetActive(true);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void ContinueGame()
    {
        _menu.SetActive(false);
    }
    private void EndGame()
    {
        Application.Quit();
    }
    private void Volume()
    {
        
    }
}
