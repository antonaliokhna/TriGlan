using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSettings : MonoBehaviour
{
    GameObject GlowMeneger;
    GameObject MusicMeneger;
    // Start is called before the first frame update
    void Start()
    {
        GlowMeneger = GameObject.Find("GlowMeneger").gameObject;
        MusicMeneger = GameObject.Find("BackroundMusicManeger").gameObject;

        if (!OptionsClick.quality)
            GlowMeneger.SetActive(false);
        else
            GlowMeneger.SetActive(true);

        if (!OptionsClick.backroundMusic)
            MusicMeneger.SetActive(false);
        else
            MusicMeneger.SetActive(true);
    }
}
