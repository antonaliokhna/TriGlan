using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class OptionsClick : MonoBehaviour
{
    private List<string> resolution = new List<string>() { "1280 x 720", "1366 x 768", "1600 x 900", "1920 x 1080", "4096 x 2160" };

    public GameObject QualityOn;
    public GameObject QualityOff;

    public GameObject FullScreenOn;
    public GameObject FullScreenOff;

    public GameObject BackroundMusicOn;
    public GameObject BackroundMusicOff;

    public Dropdown dropdownScreen;

    private UnityEngine.Color selectedButton;
    private UnityEngine.Color defaultColor;

    public static bool quality = true;
    public static bool fullScreen = true;
    public static bool backroundMusic = true;
    public static string resolutionNow = "1920 x 1080";

    public bool Quality
    {
        get => quality;
        set
        {
            quality = value;
            ChengeQuality();
            PlayerPrefs.SetString("quality", quality.ToString());
        }
    }
    public bool FullScreen
    {
        get => fullScreen; set
        {
            fullScreen = value;
            ChengeScreen();
            PlayerPrefs.SetString("fullScreen", fullScreen.ToString());
        }
    }
    public bool BackroundMusic
    {
        get => backroundMusic; set
        {
            backroundMusic = value;
            ChengeMusic();
            PlayerPrefs.SetString("backroundMusic", backroundMusic.ToString());
        }
    }

    void Start()
    {
        selectedButton = new UnityEngine.Color(0f, 0.9f, 1f, 0.55f);
        defaultColor = new UnityEngine.Color(1f, 0.3f, 0.33f, 0.45f);
        CheckPrefabsSettings();
        ChengeMusic();
        ChengeScreen();
        ChengeQuality();
        SetDropBoxValues();
    }

    private static void CheckPrefabsSettings()
    {
        var stringQuality = PlayerPrefs.GetString("quality");
        var stringFullScreen = PlayerPrefs.GetString("fullScreen");
        var stringBackroundMusic = PlayerPrefs.GetString("backroundMusic");
        var stringResolutionNow = PlayerPrefs.GetString("resolutionNow");

        if (stringQuality != "")
            quality = Convert.ToBoolean(stringQuality);
        if (stringFullScreen != "")
            fullScreen = Convert.ToBoolean(stringFullScreen);
        if (stringBackroundMusic != "")
            backroundMusic = Convert.ToBoolean(stringBackroundMusic);
        if (stringResolutionNow != "")
            resolutionNow = stringResolutionNow;
    }

    private void ChengeMusic()
    {
        if (BackroundMusic)
            ChangeGameobjectColor(BackroundMusicOn, BackroundMusicOff);
        else
            ChangeGameobjectColor(BackroundMusicOff, BackroundMusicOn);
    }

    private void ChengeScreen()
    {
        Screen.fullScreen = FullScreen;
        if (FullScreen)
            ChangeGameobjectColor(FullScreenOn, FullScreenOff);
        else
            ChangeGameobjectColor(FullScreenOff, FullScreenOn);
    }

    private void ChengeQuality()
    {
        if (Quality)
            ChangeGameobjectColor(QualityOn, QualityOff);
        else
            ChangeGameobjectColor(QualityOff, QualityOn);
    }

    private void ChangeGameobjectColor(GameObject objectSelected, GameObject objectDefault)
    {
        objectSelected.GetComponent<Image>().color = selectedButton;
        objectDefault.GetComponent<Image>().color = defaultColor;
    }

    public void OnChengeQuality(bool quality)
    {
        this.Quality = quality;
    }
    public void OnFullScreen(bool full)
    {
        this.FullScreen = full;
    }
    public void OnChengeBackroundMusic(bool boolMusic)
    {
        this.BackroundMusic = boolMusic;
    }
    public void OnChangeScreenSize()
    {
        string[] WidthAndHeight = dropdownScreen.options[dropdownScreen.value].text.Replace(" ", "").Split('x');
        Screen.SetResolution(Convert.ToInt32(WidthAndHeight[0]), Convert.ToInt32(WidthAndHeight[1]), fullScreen);
        resolutionNow = dropdownScreen.options[dropdownScreen.value].text;
        PlayerPrefs.SetString("resolutionNow", resolutionNow.ToString());
    }
    public void SetDropBoxValues()
    {
        dropdownScreen.AddOptions(resolution);
        for (int i = 0; i < resolution.Count; i++)
            if (resolution[i] == resolutionNow)
                dropdownScreen.value = i;
    }
}
