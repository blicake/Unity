using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    #region "Value"
    private int _mouseSensitivity = 5;
    private int _soundVolume = 5;

    [SerializeField] private Text _MenuPause;
    [SerializeField] private Slider _soundVolumeSlider;
    [SerializeField] private Slider _mouseSensitivitySlider;
    [SerializeField] private Button _startContinue;
    [SerializeField] private Button _options;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _closeMenu;
    [SerializeField] private Button _closeOptionsMenu;

    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _menu;
    #endregion "Value"

    #region "Property"
    public int MouseSensitivity => _mouseSensitivity;
    public int SoundVolume => _soundVolume;
    public GameObject OptionsMenu => _optionsMenu;
    #endregion "Property"

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            _soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
            _mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);

            CheckScene();

            _options.onClick.AddListener(SetOptionsMenu);
            _exit.onClick.AddListener(EndGame);
            _closeOptionsMenu.onClick.AddListener(CloseOptionsMenu);
        }
        else
            Destroy(gameObject);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _optionsMenu.activeInHierarchy)
        {
            _optionsMenu.SetActive(false);
        }
        CheckScene();
    }

    private void CheckScene()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            gameObject.SetActive(true);
            _MenuPause.GetComponent<Text>().text = "MENU";
            _startContinue.onClick.AddListener(StartGame);
            _startContinue.GetComponentInChildren<Text>().text = "Start";
            _closeMenu.gameObject.SetActive(false);
        }
        else
        {
            _MenuPause.GetComponent<Text>().text = "PAUSE";
            _closeMenu.gameObject.SetActive(true);
            _closeMenu.onClick.AddListener(CloseMenu);
            _startContinue.onClick.RemoveListener(StartGame);
            _startContinue.onClick.AddListener(ContinueGame);
            _startContinue.GetComponentInChildren<Text>().text = "Continue";
            gameObject.SetActive(false);
        }
    }

    private void CloseOptionsMenu()
    {
        _optionsMenu.SetActive(false);
    }

    private void CloseMenu()
    {
        _menu.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    private void SetOptionsMenu()
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
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    private void EndGame()
    {
        Application.Quit();
    }

    private void SetMouseSensitivity(float value)
    {
        instance._mouseSensitivity = (int)value;
    }

    private void SetSoundVolume(float value)
    {
        AudioListener.volume = value;
        instance._soundVolume = (int)value;
    }

}
