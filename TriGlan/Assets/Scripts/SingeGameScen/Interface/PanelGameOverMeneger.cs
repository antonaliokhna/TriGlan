using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelGameOverMeneger : MonoBehaviour
{
    public Animator[] AnimatorsGameOverPanel;
    public InterfaceManager interfaceManager;
    public GameObject MainCanvas;
    public void GameOverStartAnimation()
    {
        MainCanvas.SetActive(false);
        this.gameObject.SetActive(true);
        foreach (Animator animator in AnimatorsGameOverPanel)
            SetTriggerAnimation(animator, "Start");
    }

    private void SetTriggerAnimation(Animator anim, string trigger)
    {
        anim.SetTrigger(trigger);
    }
    
    public void GameOverRestartAnimatoin()
    {
        foreach (Animator animator in AnimatorsGameOverPanel)
            SetTriggerAnimation(animator, "Restart");
        StartCoroutine(WaitForSecon(0.5f));
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    IEnumerator WaitForSecon(float second)
    {
        yield return new WaitForSeconds(second);
        SceneManager.LoadScene(2);
    }
}
