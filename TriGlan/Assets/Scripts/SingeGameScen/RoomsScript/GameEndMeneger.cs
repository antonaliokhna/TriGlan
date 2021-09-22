using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndMeneger : MonoBehaviour
{
    public TimeSpan time;
    public GameObject IntefaceMeneger;
    public GameObject CanvasGameEnd;
    public GameObject[] zvezds;
    public Sprite prefubYellowStart;
    // Start is called before the first frame update
    void Start()
    {
        CanvasGameEnd.gameObject.SetActive(true);
        time = IntefaceMeneger.GetComponent<InterfaceManager>().time;

        if (time.TotalMinutes < 40d)
        {
            for (int i = 0; i < 1; i++)
            {
                zvezds[i].GetComponent<SpriteRenderer>().sprite = prefubYellowStart;
            }
        }

        if (time.TotalMinutes < 30d)
        {
            for (int i = 0; i < 2; i++)
            {
                zvezds[i].GetComponent<SpriteRenderer>().sprite = prefubYellowStart;
            }
        }

        if (time.TotalMinutes < 20d)
        {
            for (int i = 0; i < 3; i++)
            {
                zvezds[i].GetComponent<SpriteRenderer>().sprite = prefubYellowStart;
            }
        }
    }
}
