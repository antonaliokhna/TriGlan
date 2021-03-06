using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickMainOnMenu : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SpawScen(int scen)
    {
        SceneManager.LoadScene(scen);
    }
}
