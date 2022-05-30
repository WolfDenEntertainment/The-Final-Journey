using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int randomSeed;

    //[Header("UI Elements")]
    //[SerializeField] GameObject options;
    //[SerializeField] GameObject buttons;
    //[SerializeField] TMP_Dropdown qualityLevel;
    //[SerializeField] TMP_Dropdown resolutions;
    //[SerializeField] Toggle fullscreen;
    
    // Private fields
    int quality;
    int currentResolution;
    float musicVolume;
    float sfxVolume;
    bool isFullScreen = true;


    // Start is called before the first frame update
    void Start()
    {        
        Init();

        DontDestroyOnLoad(this);
    }

    void Init()
    {
        LoadSettings();

        if (randomSeed == 0)
            Random.InitState(System.DateTime.Now.Second);
        else
            Random.InitState(randomSeed);
    }

    //void GetResolutions()
    //{
    //    resolutions.ClearOptions();
    //    Resolution[] _resolutions;

    //    _resolutions = Screen.resolutions;
    //    for (int i = 0; i < _resolutions.Length; i++)
    //    {
    //        string text = ResToString(_resolutions[i]);
    //        resolutions.options.Add(new TMP_Dropdown.OptionData(text));

    //        if (_resolutions[i].Equals(Screen.currentResolution))
    //        {
    //            currentResolution = i;
    //        }
    //    }

    //    resolutions.value = currentResolution;
    //    resolutions.RefreshShownValue();
    //}

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Gameplay");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    //public void Options()
    //{
    //    buttons.SetActive(false);
    //    options.SetActive(true);
    //}

    public void Exit()
    {
        Application.Quit();
    }

    public void SetMusicVolume(float _volume)
    {
        musicVolume = _volume;
    }

    public void SetSFXVolume(float _volume)
    {
        sfxVolume = _volume;
    }

    public void SetQualityLevel(int index)
    {
        quality = QualitySettings.GetQualityLevel();        
    }

    public void SetFullScreen(bool _isFullscreen)
    {
        isFullScreen = _isFullscreen;
    }

    void LoadSettings()
    {
    }

    //public void SaveButton()
    //{
    //    buttons.SetActive(true); 
    //    options.SetActive(false);
    //}
}
