using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutScenMeneger : MonoBehaviour
{
    public GameObject[] VisibleLine;
    private float DeltaTimeAnimation = 0f;

    void Update()
    {
        VisibleLineAnimation();
    }
    private void VisibleLineAnimation()
    {
        if (DeltaTimeAnimation >= 0.5f)
        {
            if (VisibleLine[0].gameObject.activeSelf)
                SetBoolValueLines(false);
            else
                SetBoolValueLines(true);

            DeltaTimeAnimation = 0f;
        }
        else
            DeltaTimeAnimation += Time.deltaTime;
    }
    private void SetBoolValueLines(bool val)
    {
        foreach (var line in VisibleLine)
            line.gameObject.SetActive(val);
    }
}
