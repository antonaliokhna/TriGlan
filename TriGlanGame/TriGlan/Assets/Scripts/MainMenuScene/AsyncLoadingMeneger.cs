using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadingMeneger : MonoBehaviour
{
    public GameObject AsyncLoadingPanel;
    public Text textLoading;
    public Image ProgresBar;

    public void AsyncLoadingScen(int ScenNumber)
    {
        AsyncLoadingPanel.SetActive(true);
        AsyncOperation LoadingScen = SceneManager.LoadSceneAsync(ScenNumber);
        StartCoroutine(CheckLoadingIsDone(LoadingScen));
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
