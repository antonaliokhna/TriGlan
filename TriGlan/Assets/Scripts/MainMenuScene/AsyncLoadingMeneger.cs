using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadingMeneger : MonoBehaviour
{
    public GameObject audioPrefub;

    public GameObject AsyncLoadingPanel;
    public Text textLoading;
    public Image ProgresBar;


    public void Start()
    {
        if (BackroundMusicMainMenu.instansiteAudio == null && OptionsClick.backroundMusic)
            DontDestroyOnLoad(BackroundMusicMainMenu.instansiteAudio = Instantiate(audioPrefub));
    }

    public void AsyncLoadingScen(int ScenNumber)
    {
        AsyncLoadingPanel.SetActive(true);
        AsyncOperation LoadingScen = SceneManager.LoadSceneAsync(ScenNumber);
        StartCoroutine(CheckLoadingIsDone(LoadingScen));
        if (ScenNumber == 2)
            Destroy(BackroundMusicMainMenu.instansiteAudio);
    }

    private IEnumerator CheckLoadingIsDone(AsyncOperation LoadingScen)
    {
        while (LoadingScen.progress != 0.9f)
        {
            SetProgresValues(Mathf.Round(LoadingScen.progress));
            yield return new WaitForSeconds(0.01f);
        }
        SetProgresValues(Mathf.Round(LoadingScen.progress));
    }
    private void SetProgresValues(float valueLoading)
    {
        ProgresBar.fillAmount = Mathf.Round(valueLoading);
        textLoading.text = $"Loading... {Mathf.Round(valueLoading) * 100}%";
    }
}
