using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundMusicMainMenu : MonoBehaviour
{
    public GameObject audioPrefubMainMenu;
    public static GameObject instansiteAudio;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instansiteAudio == null && OptionsClick.backroundMusic)
            DontDestroyOnLoad(instansiteAudio = Instantiate(audioPrefubMainMenu));
    }
}